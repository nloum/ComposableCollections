using System;
using SimpleMonads;

namespace IoFluently
{
    public class FileOrMissingPath : FileOrMissingPath<File, MissingPath>, IFileOrMissingPath
    {
        public FileOrMissingPath(IAbsolutePath path) : base(path)
        {
        }

        public FileOrMissingPath(IEither<File, MissingPath> source) : base(source)
        {
        }
    }
    
    public class FileOrMissingPath<TFile, TMissingPath> : FileOrFolderOrMissingPath<TFile, Folder, TMissingPath>, IFileOrMissingPath<TFile, TMissingPath>
        where TFile : File where TMissingPath : MissingPath
    {
        private IEither<TFile, TMissingPath> _reduce => this.Collapse(
            file => new Either<TFile, TMissingPath>(file),
            folder => throw new InvalidOperationException(),
            missingPath => new Either<TFile, TMissingPath>(missingPath));
    
        public FileOrMissingPath(IAbsolutePath path) : base(path)
        {
        }

        public FileOrMissingPath(IEither<TFile, TMissingPath> source)
            : base(source.Collapse(file => (IFileOrFolderOrMissingPath<TFile, Folder, TMissingPath>)file, missingPath => (IFileOrFolderOrMissingPath<TFile, Folder, TMissingPath>)missingPath))
        {
        }

        public TFile? Item1 => _reduce.Item1;

        public TMissingPath? Item2 => _reduce.Item2;

        public IEither<TFile, TMissingPath, T3> Or<T3>()
        {
            return _reduce.Or<T3>();
        }

        public IEither<TFile, TMissingPath, T3, T4> Or<T3, T4>()
        {
            return _reduce.Or<T3, T4>();
        }

        public IEither<TFile, TMissingPath, T3, T4, T5> Or<T3, T4, T5>()
        {
            return _reduce.Or<T3, T4, T5>();
        }

        public IEither<TFile, TMissingPath, T3, T4, T5, T6> Or<T3, T4, T5, T6>()
        {
            return _reduce.Or<T3, T4, T5, T6>();
        }

        public IEither<TFile, TMissingPath, T3, T4, T5, T6, T7> Or<T3, T4, T5, T6, T7>()
        {
            return _reduce.Or<T3, T4, T5, T6, T7>();
        }

        public IEither<TFile, TMissingPath, T3, T4, T5, T6, T7, T8> Or<T3, T4, T5, T6, T7, T8>()
        {
            return _reduce.Or<T3, T4, T5, T6, T7, T8>();
        }

        public IEither<TFile, TMissingPath, T3, T4, T5, T6, T7, T8, T9> Or<T3, T4, T5, T6, T7, T8, T9>()
        {
            return _reduce.Or<T3, T4, T5, T6, T7, T8, T9>();
        }

        public IEither<TFile, TMissingPath, T3, T4, T5, T6, T7, T8, T9, T10> Or<T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            return _reduce.Or<T3, T4, T5, T6, T7, T8, T9, T10>();
        }

        public IEither<TFile, TMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            return _reduce.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        }

        public IEither<TFile, TMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            return _reduce.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        }

        public IEither<TFile, TMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            return _reduce.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        }

        public IEither<TFile, TMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            return _reduce.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        }

        public IEither<TFile, TMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            return _reduce.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        }

        public IEither<TFile, TMissingPath, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            return _reduce.Or<T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        }

        public TOutput Collapse<TOutput>(Func<TFile, TOutput> selector1, Func<TMissingPath, TOutput> selector2)
        {
            return _reduce.Collapse(selector1, selector2);
        }

        public ConvertibleTo<TBase>.IEither<TFile, TMissingPath> ConvertTo<TBase>()
        {
            return _reduce.ConvertTo<TBase>();
        }
    }
}