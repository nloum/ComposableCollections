using SimpleMonads;

namespace DebuggableSourceGenerators.Write
{
    public class MethodImplementationMode : Either<StaticMethodImplementation, DelegateFromConstructorParameter,
        UnchangingReturnValueFromConstructorParameter>
    {
        public MethodImplementationMode(StaticMethodImplementation item1) : base(item1)
        {
        }

        public MethodImplementationMode(DelegateFromConstructorParameter item2) : base(item2)
        {
        }

        public MethodImplementationMode(UnchangingReturnValueFromConstructorParameter item3) : base(item3)
        {
        }
    }
}