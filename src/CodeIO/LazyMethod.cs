using System;
using System.Collections.Generic;

namespace CodeIO
{
    public class LazyMethod : IMethod
    {
        private Lazy<IType> _returnType;

        public LazyMethod(Lazy<IType> returnType, string name, Visibility visibility, IReadOnlyList<IParameter> parameters, bool isStatic)
        {
            _returnType = returnType;
            Name = name;
            Visibility = visibility;
            Parameters = parameters;
            IsStatic = isStatic;
        }

        public string Name { get; }
        public IType ReturnType => _returnType.Value;
        public Visibility Visibility { get; }
        public bool IsStatic { get; }
        public IReadOnlyList<IParameter> Parameters { get; }
        
    }
}