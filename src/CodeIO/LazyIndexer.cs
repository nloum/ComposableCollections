using System;
using System.Collections.Generic;

namespace CodeIO
{
    public class LazyIndexer : IIndexer
    {
        private readonly Lazy<IType> _returnType;

        public LazyIndexer(Lazy<IType> returnType, Visibility visibility, IReadOnlyList<IParameter> parameters)
        {
            _returnType = returnType;
            Visibility = visibility;
            Parameters = parameters;
        }

        public IType ReturnType => _returnType.Value;
        public IReadOnlyList<IParameter> Parameters { get; }
        public Visibility Visibility { get; }
    }
}