using Autofac;
using IoFluently;

namespace ComposableCollections.CodeGenerator
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AnonymousImplementationGenerator>()
                .Keyed<GeneratorBase>(typeof(AnonymousImplementationGeneratorSettings));
            builder.RegisterType<CombinationInterfacesGenerator>()
                .Keyed<GeneratorBase>(typeof(CombinationInterfacesGeneratorSettings));
            builder.RegisterType<ConstructorToExtensionMethodGenerator>()
                .Keyed<GeneratorBase>(typeof(ConstructorToExtensionMethodGeneratorSettings));
            builder.RegisterType<DecoratorBaseGenerator>()
                .Keyed<GeneratorBase>(typeof(DecoratorBaseGeneratorSettings));
            builder.RegisterType<SubclassCombinationImplementationGenerator>()
                .Keyed<GeneratorBase>(typeof(SubclassCombinationImplementationGeneratorSettings));
            builder.Register<IIoService>(_ => new IoService(false)).SingleInstance();
            builder.RegisterType<CommandLineService>().AsImplementedInterfaces().SingleInstance();
        }
    }
}