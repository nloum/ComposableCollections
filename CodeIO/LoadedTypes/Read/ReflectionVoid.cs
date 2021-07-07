using System;

namespace CodeIO.LoadedTypes.Read
{
    public class ReflectionVoid : Void, IReflectionType
    {
        public static ReflectionVoid Instance { get; } = new ReflectionVoid();
        
        private ReflectionVoid()
        {
            
        }

        public Type Type { get; } = typeof(void);
    }
}