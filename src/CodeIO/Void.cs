namespace CodeIO
{
    public abstract class Void : IType
    {
        protected Void()
        {
            
        }
        
        public TypeIdentifier Identifier { get; } = new TypeIdentifier()
        {
            Arity = 0,
            Name = "Void",
            Namespace = "System"
        };

        public Visibility Visibility { get; } = Visibility.Public;
    }
}