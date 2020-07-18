using Autofac;
using ReactiveProcesses;
using Serilog;

namespace IoFluently.Examples.RemoveDuplicates
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<ILogger>(_ =>
            {
                var logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .CreateLogger();

                return logger;
            });
            builder.RegisterType<ReactiveProcessFactory>().As<IReactiveProcessFactory>().SingleInstance();
            builder.RegisterType<IoService>().As<IIoService>().SingleInstance();
            builder.RegisterType<RemoveDuplicatesService>().As<IRemoveDuplicatesService>().SingleInstance();
        }
    }
}