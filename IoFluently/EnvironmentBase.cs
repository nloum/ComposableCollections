﻿using System;
using LiveLinq.Set;

namespace IoFluently
{
    /// <summary>
    /// Partial implementation of <see cref="IEnvironment"/>, useful for creating a new implementation
    /// </summary>
    public abstract class EnvironmentBase : FileSystemBase, IEnvironment
    {
        protected EnvironmentBase(IOpenFilesTrackingService openFilesTrackingService, bool isCaseSensitiveByDefault, string defaultDirectorySeparator) : base(openFilesTrackingService, isCaseSensitiveByDefault, defaultDirectorySeparator)
        {
        }

        /// <inheritdoc />
        public virtual FolderPath CurrentDirectory { get; set; }

        /// <inheritdoc />
        public virtual FolderPath TemporaryFolder { get; set; }

        /// <inheritdoc />
        public MissingPath GenerateUniqueTemporaryPath(string extension = null)
        {
            var result = TemporaryFolder / Guid.NewGuid().ToString();
            if (!string.IsNullOrEmpty(extension))
            {
                result = result.WithExtension(extension);
            }

            return new MissingPath(result);
        }
        
        /// <inheritdoc />
        public virtual FileOrFolderOrMissingPath ParsePathRelativeToWorkingDirectory(string path)
        {
            return TryParseAbsolutePath(path, CurrentDirectory).Value;
        }
    }
}