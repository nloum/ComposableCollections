using System;
using System.Collections.Immutable;
using LiveLinq.Set;

namespace IoFluently
{
    public class MirrorFileSystem : PathTransformationFileSystemBase
    {
        private readonly IFileSystem _decorated;
        private readonly ObservableSet<FolderPath> _roots = new();
        
        public ImmutableDictionary<AbsolutePath, AbsolutePath> Mappings { get; set; } = ImmutableDictionary<AbsolutePath, AbsolutePath>.Empty;
        
        public MirrorFileSystem(IFileSystem decorated, string defaultRoot) : base(new OpenFilesTrackingService(decorated.OpenFilesTrackingService.IsEnabled),
            decorated.IsCaseSensitiveByDefault, decorated.DefaultDirectorySeparator, decorated.CanEmptyDirectoriesExist, decorated.EmptyFolderMode)
        {
            _decorated = decorated;
            var defaultRootParsed = decorated.ParseAbsolutePath(defaultRoot);
            var defaultRootFolder = new FolderPath(defaultRootParsed.Components, defaultRootParsed.IsCaseSensitive,
                defaultRootParsed.DirectorySeparator, this, false);
            DefaultRoot = defaultRootFolder;
            _roots.Add(defaultRootFolder);
        }

        public override IObservableReadOnlySet<FolderPath> Roots => _roots;
        public override void UpdateRoots()
        {
            
        }

        public override FolderPath DefaultRoot { get; }

        protected override AbsolutePath Transform(IFileOrFolderOrMissingPath absolutePath)
        {
            var absolutePathString = absolutePath.FullName;
            var stringComparison =
                IsCaseSensitiveByDefault ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            foreach (var mapping in Mappings)
            {
                if (absolutePathString.Equals(mapping.Key.FullName, stringComparison)
                    || absolutePathString.StartsWith(mapping.Key.FullName, stringComparison))
                {
                    var transformedPath = mapping.Value / absolutePath.RelativeTo(mapping.Key);
                    return new AbsolutePath(transformedPath.Components, transformedPath.IsCaseSensitive, transformedPath.DirectorySeparator, _decorated);
                }
            }

            return absolutePath.ExpectFileOrFolderOrMissingPath();
        }
    }
}