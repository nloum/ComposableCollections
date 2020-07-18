using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Autofac;
using CommandLine;

namespace IoFluently.Examples.RemoveDuplicates
{
    [Verb("find")]
    public class BuildPlanVerb
    {
        public BuildPlanVerb(IEnumerable<string> rootFolders, string planPath)
        {
            RootFolders = rootFolders;
            PlanPath = planPath;
        }

        [Value(0)]
        public IEnumerable<string> RootFolders { get; }
        
        [Option('p', "plan", Required = true)]
        public string PlanPath { get; }
    }
    
    [Verb("execute")]
    public class ExecutePlanVerb
    {
        public ExecutePlanVerb(string planPath)
        {
            PlanPath = planPath;
        }

        [Option('p', "plan", Required = true)]
        public string PlanPath { get; }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<BuildPlanVerb, ExecutePlanVerb>(args)
                .WithParsed<BuildPlanVerb>(o =>
                {
                    Run(o);
                })
                .WithParsed<ExecutePlanVerb>(o =>
                {
                    Run(o);
                });
        }

        private static IContainer BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<AutofacModule>();
            return containerBuilder.Build();
        }
        
        private static void Run(BuildPlanVerb options)
        {
            var container = BuildContainer();

            var service = container.Resolve<IRemoveDuplicatesService>();
            var ioService = container.Resolve<IIoService>();

            var duplicates = service.FindDuplicates(options.RootFolders.Select(x => ioService.ParseAbsolutePath(x)));

            var planFile = ioService.ParseAbsolutePath(options.PlanPath).AsDuplicateRemovalPlan();
            planFile.Write(duplicates);
        }

        private static void Run(ExecutePlanVerb options)
        {
            var container = BuildContainer();
            
            var service = container.Resolve<IRemoveDuplicatesService>();
            var ioService = container.Resolve<IIoService>();
            
            var planFile = ioService.ParseAbsolutePath(options.PlanPath).AsDuplicateRemovalPlan();
            var plan = planFile.Read();

            service.Execute(plan);
        }
    }
}