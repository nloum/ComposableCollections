using SimpleMonads;

namespace DebuggableSourceGenerators.Write
{
    public class StaticMethodParameterImplementation : Either<StaticMethodParameterPullFromConstructor, StaticMethodParameterIncludeInMethodImplementation, StaticMethodParameterConstant>
    {
        public StaticMethodParameterImplementation(StaticMethodParameterPullFromConstructor item1) : base(item1)
        {
        }

        public StaticMethodParameterImplementation(StaticMethodParameterIncludeInMethodImplementation item2) : base(item2)
        {
        }

        public StaticMethodParameterImplementation(StaticMethodParameterConstant item3) : base(item3)
        {
        }
    }
}