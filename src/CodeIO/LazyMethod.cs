using System;
using System.Collections.Generic;

namespace CodeIO
{
    public class LazyMethod : IMethod
    {
        private Lazy<IType> _returnType;

        public LazyMethod(Lazy<IType> returnType, string name, Visibility visibility, IReadOnlyList<IParameter> parameters)
        {
            _returnType = returnType;
            Name = name;
            Visibility = visibility;
            Parameters = parameters;
        }

        public string Name { get; }
        public IType ReturnType => _returnType.Value;
        public Visibility Visibility { get; }
        public IReadOnlyList<IParameter> Parameters { get; }
    }
}