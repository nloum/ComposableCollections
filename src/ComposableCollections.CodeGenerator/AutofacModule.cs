using Autofac;
using IoFluently;

namespace ComposableCollections.CodeGenerator
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AnonymousImplementationsGenerator>()
                .Keyed<GeneratorBase>(typeof(AnonymousImplementationsGeneratorSettings));
            builder.RegisterType<CombinationInterfacesGenerator>()
                .Keyed<GeneratorBase>(typeof(CombinationInterfacesGeneratorSettings));
            builder.RegisterType<ConstructorExtensionMethodsGenerator>()
                .Keyed<GeneratorBase>(typeof(ConstructorExtensionMethodsGeneratorSettings));
            builder.RegisterType<DecoratorBaseClassesGenerator>()
                .Keyed<GeneratorBase>(typeof(DecoratorBaseClassesGeneratorSettings));
            builder.RegisterType<SubclassCombinationImplementationsGenerator>()
                .Keyed<GeneratorBase>(typeof(SubclassCombinationImplementationsGeneratorSettings));
            
            builder.Register<IIoService>(_ => new IoService(false)).SingleInstance();
            builder.RegisterType<CommandLineService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<PathService>().AsImplementedInterfaces().SingleInstance();
        }
    }
}