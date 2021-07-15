using System;
using System.Collections.Immutable;
using System.Linq;
using LiveLinq.Set;

namespace IoFluently
{
    public class UnionFileSystem : PathTransformationFileSystemBase
    {
        private readonly ObservableSet<Folder> _roots = new();
        public ImmutableList<IFileSystem> IoServices { get; set; } = ImmutableList<IFileSystem>.Empty;

        public UnionFileSystem(IOpenFilesTrackingService openFilesTrackingService, bool isCaseSensitiveByDefault, string defaultDirectorySeparator) : base(openFilesTrackingService, isCaseSensitiveByDefault, defaultDirectorySeparator)
        {
        }

        public override IObservableReadOnlySet<Folder> Roots => _roots;
        public override void UpdateRoots()
        {
            _roots.RemoveRange(_roots);
            
            foreach (var ioService in IoServices)
            {
                ioService.UpdateRoots();
                _roots.AddRange(ioService.Roots);
            }
        }

        public override Folder DefaultRoot => IoServices.First().DefaultRoot;
        public override EmptyFolderMode EmptyFolderMode => IoServices.First().EmptyFolderMode;

        public override bool CanEmptyDirectoriesExist =>
            IoServices.Any(ioService => ioService.CanEmptyDirectoriesExist);

        protected override AbsolutePath Transform(IFileOrFolderOrMissingPath absolutePath)
        {
            AbsolutePath? tmpPath = null;
            
            foreach (var ioService in IoServices)
            {
                tmpPath = new AbsolutePath(absolutePath.Components, absolutePath.IsCaseSensitive,
                    absolutePath.DirectorySeparator, ioService);
                if (tmpPath.Exists)
                {
                    return tmpPath;
                }
            }

            if (tmpPath == null)
            {
                throw new InvalidOperationException("No IIoServices were specified to the Union IoService");
            }
            
            return tmpPath;
        }
    }
}