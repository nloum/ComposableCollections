using System.Collections.Immutable;

namespace FluentSourceGenerators
{
    public abstract class GeneratorBase
    {
        public abstract void NonGenericInitialize(object settings);

        public abstract ImmutableDictionary<string, string> Generate(CodeIndexerService codeIndexerService);
    }
    
    public abstract class GeneratorBase<TSettings> : GeneratorBase
    {
        public override void NonGenericInitialize(object settings)
        {
            Initialize((TSettings)settings);
        }

        public abstract void Initialize(TSettings settings);
    }
}