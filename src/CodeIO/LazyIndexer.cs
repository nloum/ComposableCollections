using System;
using System.Collections.Generic;

namespace CodeIO
{
    public class LazyIndexer : IIndexer
    {
        private readonly Lazy<IType> _returnType;

        public LazyIndexer(Lazy<IType> returnType, IReadOnlyList<IParameter> parameters, Visibility? getterVisibility, Visibility? setterVisibility, bool isStatic)
        {
            _returnType = returnType;
            Parameters = parameters;
            GetterVisibility = getterVisibility;
            SetterVisibility = setterVisibility;
            IsStatic = isStatic;
        }

        public IType ReturnType => _returnType.Value;
        public IReadOnlyList<IParameter> Parameters { get; }
        public Visibility? GetterVisibility { get; }
        public Visibility? SetterVisibility { get; }
        public bool IsStatic { get; }
    }
}