using System;
using System.Collections.Immutable;
using System.Linq;
using LiveLinq.Set;

namespace IoFluently
{
    public class UnionFileSystem : PathTransformationFileSystemBase
    {
        private readonly ObservableSet<FolderPath> _roots = new();
        public ImmutableList<IFileSystem> IoServices { get; set; } = ImmutableList<IFileSystem>.Empty;

        public UnionFileSystem(IOpenFilesTrackingService openFilesTrackingService, bool isCaseSensitiveByDefault, string defaultDirectorySeparator) : base(openFilesTrackingService, isCaseSensitiveByDefault, defaultDirectorySeparator)
        {
        }

        public override IObservableReadOnlySet<FolderPath> Roots => _roots;
        public override void UpdateRoots()
        {
            _roots.RemoveRange(_roots);
            
            foreach (var ioService in IoServices)
            {
                ioService.UpdateRoots();
                _roots.AddRange(ioService.Roots);
            }
        }

        public override FolderPath DefaultRoot => IoServices.First().DefaultRoot;
        public override EmptyFolderMode EmptyFolderMode => IoServices.First().EmptyFolderMode;

        public override bool CanEmptyDirectoriesExist =>
            IoServices.Any(ioService => ioService.CanEmptyDirectoriesExist);

        protected override FileOrFolderOrMissingPath Transform(IFileOrFolderOrMissingPath absolutePath)
        {
            FileOrFolderOrMissingPath? tmpPath = null;
            
            foreach (var ioService in IoServices)
            {
                tmpPath = new FileOrFolderOrMissingPath(absolutePath.Components, absolutePath.IsCaseSensitive,
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