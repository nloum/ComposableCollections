using Autofac;
using CommandLine;

namespace IoFluently.Examples.RemoveDuplicates
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = BuildContainer();
            
            Parser.Default.ParseArguments<BuildPlanVerb, ExecutePlanVerb>(args)
                .WithParsed<BuildPlanVerb>(o =>
                {
                    var service = container.Resolve<ICommandLineService<BuildPlanVerb>>();
                    service.Run(o);
                })
                .WithParsed<ExecutePlanVerb>(o =>
                {
                    var service = container.Resolve<ICommandLineService<ExecutePlanVerb>>();
                    service.Run(o);
                });
        }

        private static IContainer BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<AutofacModule>();
            return containerBuilder.Build();
        }
    }
}