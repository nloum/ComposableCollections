using System;
using SimpleMonads;

namespace IoFluently
{
    public class FileOrFolder : FileOrFolderOrMissingPath, IFileOrFolder
    {
        private SubTypesOf<IHasAbsolutePath>.Either<File, Folder> _value;

        public FileOrFolder(IFileOrFolderOrMissingPath path) : base(path)
        {
            _value = path.Collapse(
                file => new SubTypesOf<IHasAbsolutePath>.Either<File, Folder>(file),
                folder => new SubTypesOf<IHasAbsolutePath>.Either<File, Folder>(folder),
                missingPath => throw new InvalidOperationException());
        }

        public TOutput Collapse<TOutput>(Func<File, TOutput> selector1, Func<Folder, TOutput> selector2)
        {
            return _value.Collapse(selector1, selector2);
        }

        public IEither<File, Folder, T3> Or<T3>()
        {
            return _value.Or<T3>();
        }

        public IEither<File, Folder, T3, T4> Or<T3, T4>()
        {
            return _value.Or<T3, T4>();
        }

        public IEither<File, Folder, T3, T4, T5> Or<T3, T4, T5>()
        {
            return _value.Or<T3, T4, T5>();
        }

        public IEither<File, Folder, T3, T4, T5, T6> Or<T3, T4, T5, T6>()
        {
            return _value.Or<T3, T4, T5, T6>();
        }

        public IEither<File, Folder, T3, T4, T5, T6, T7> Or<T3, T4, T5, T6, T7>()
        {
            return _value.Or<T3, T4, T5, T6, T7>();
        }

        public IEither<File, Folder, T3, T4, T5, T6, T7, T8> Or<T3, T4, T5, T6, T7, T8>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8>();
        }

        public IEither<File, Folder, T3, T4, T5, T6, T7, T8, T9> Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9>();
        }

        public IEither<File, Folder, T3, T4, T5, T6, T7, T8, T9, T10> Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }

        public IEither<File, Folder, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        public IEither<File, Folder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        public IEither<File, Folder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        public IEither<File, Folder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        public IEither<File, Folder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        public IEither<File, Folder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        public ConvertibleTo<TBase>.IEither<File, Folder> ConvertTo<TBase>()
        {
            return _value.ConvertTo<TBase>();
        }
    }
}