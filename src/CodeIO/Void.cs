namespace CodeIO
{
    public class Void : IType
    {
        public static Void Instance { get; } = new Void();
        
        private Void()
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