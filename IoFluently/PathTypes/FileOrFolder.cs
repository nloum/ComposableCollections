using System;
using SimpleMonads;

namespace IoFluently
{
    public class FileOrFolder : FileOrFolder<File, Folder>
    {
        public FileOrFolder(File item1) : base(item1)
        {
        }

        public FileOrFolder(Folder item2) : base(item2)
        {
        }

        public FileOrFolder(IFileOrFolderOrMissingPath<File, Folder, MissingPath> path) : base(path)
        {
        }
    }
    
    public class FileOrFolder<TFile, TFolder> : FileOrFolderOrMissingPath<TFile, TFolder, MissingPath>,
        IFileOrFolderOrMissingPath<TFile, TFolder, MissingPath>,
        IFileOrFolder<TFile, TFolder>
        where TFile : File<TFile> where TFolder : Folder<TFolder>
    {
        private Either<TFile, TFolder> _value;

        public FileOrFolder(TFile item1) : base(item1)
        {
            _value = new Either<TFile, TFolder>(item1);
        }

        public FileOrFolder(TFolder item2) : base(item2)
        {
            _value = new Either<TFile, TFolder>(item2);
        }

        public FileOrFolder(IFileOrFolderOrMissingPath<TFile, TFolder, MissingPath> path) : base(path)
        {
            _value = path.Collapse(
                file => new Either<TFile, TFolder>(file),
                folder => new Either<TFile, TFolder>(folder),
                missingPath => throw new InvalidOperationException());
        }

        public TOutput Collapse<TOutput>(Func<TFile, TOutput> selector1, Func<TFolder, TOutput> selector2)
        {
            return _value.Collapse(selector1, selector2);
        }

        public IEither<TFile, TFolder, T3> Or<T3>()
        {
            return _value.Or<T3>();
        }

        public IEither<TFile, TFolder, T3, T4> Or<T3, T4>()
        {
            return _value.Or<T3, T4>();
        }

        public IEither<TFile, TFolder, T3, T4, T5> Or<T3, T4, T5>()
        {
            return _value.Or<T3, T4, T5>();
        }

        public IEither<TFile, TFolder, T3, T4, T5, T6> Or<T3, T4, T5, T6>()
        {
            return _value.Or<T3, T4, T5, T6>();
        }

        public IEither<TFile, TFolder, T3, T4, T5, T6, T7> Or<T3, T4, T5, T6, T7>()
        {
            return _value.Or<T3, T4, T5, T6, T7>();
        }

        public IEither<TFile, TFolder, T3, T4, T5, T6, T7, T8> Or<T3, T4, T5, T6, T7, T8>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8>();
        }

        public IEither<TFile, TFolder, T3, T4, T5, T6, T7, T8, T9> Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9>();
        }

        public IEither<TFile, TFolder, T3, T4, T5, T6, T7, T8, T9, T10> Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }

        public IEither<TFile, TFolder, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        public IEither<TFile, TFolder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        public IEither<TFile, TFolder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        public IEither<TFile, TFolder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        public IEither<TFile, TFolder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        public IEither<TFile, TFolder, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return _value.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        public ConvertibleTo<TBase>.IEither<TFile, TFolder> ConvertTo<TBase>()
        {
            return _value.ConvertTo<TBase>();
        }
    }
}