using System;
using SimpleMonads;

namespace IoFluently
{
    public partial class MissingPath : FileOrMissingPath, IFileOrMissingPath, IFolderOrMissingPath
    {
        private SubTypesOf<IHasAbsolutePath>.Either<Folder, MissingPath> _value;
    
        public MissingPath(IFileOrFolderOrMissingPath path) : base(path)
        {
            _value = path.Collapse(
                file => throw new InvalidOperationException(),
                folder => new SubTypesOf<IHasAbsolutePath>.Either<Folder, MissingPath>(folder),
                missingPath => new SubTypesOf<IHasAbsolutePath>.Either<Folder, MissingPath>(missingPath));
        }
        
        public Folder? Item1 => _value.Item1;

        public MissingPath? Item2 => _value.Item2;

        public TOutput Collapse<TOutput>(Func<Folder, TOutput> selector1, Func<MissingPath, TOutput> selector2)
        {
            return _value.Collapse(selector1, selector2);
        }

        public IEither<Folder, MissingPath, T3> Or<T3>()
        {
            return _value.Or<T3>();
        }

        public IEither<Folder, MissingPath, T3, T4> Or<T3, T4>()
        {
            return _value.Or<T3, T4>();
        }

        public IEither<Folder, MissingPath, T3, T4, T5> Or<T3, T4, T5>()
        {
            return _value.Or<T3, T4, T5>();
        }

        public IEither<Folder, MissingPath, T3, T4, T5, T6> Or<T3, T4, T5, T6>()
        {
            return _value.Or<T3, T4, T5, T6>();
        }

        public IEither<Folder, MissingPath, T3, T4, T5, T6, T7> Or<T3, T4, T5, T6, T7>()
        {
            return _value.Or<T3, T4, T5, T6, T7>();
        }

        public IEither<Folder, MissingPath, T3, T4, T5, T6, T7, T8> Or<T3, T4, T5, T6, T7, T8>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8>();
        }

        public IEither<Folder, MissingPath, T3, T4, T5, T6, T7, T8, T9> Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9>();
        }

        public IEither<Folder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10> Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }

        public IEither<Folder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        public IEither<Folder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        public IEither<Folder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        public IEither<Folder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        public IEither<Folder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        public IEither<Folder, MissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        public ConvertibleTo<TBase>.IEither<Folder, MissingPath> ConvertTo<TBase>()
        {
            return _value.ConvertTo<TBase>();
        }
    }
}