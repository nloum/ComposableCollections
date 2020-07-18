using System.Linq;
using Autofac;
using CommandLine;

namespace IoFluently.Examples.RemoveDuplicates
{
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