using System;
using LiveLinq.Set;

namespace IoFluently
{
    /// <summary>
    /// Partial implementation of <see cref="IIoEnvironmentService"/>, useful for creating a new implementation
    /// </summary>
    public abstract class IoEnvironmentServiceBase : IoServiceBase, IIoEnvironmentService
    {
        protected IoEnvironmentServiceBase(IOpenFilesTrackingService openFilesTrackingService, bool isCaseSensitiveByDefault, string defaultDirectorySeparator) : base(openFilesTrackingService, isCaseSensitiveByDefault, defaultDirectorySeparator)
        {
        }

        /// <inheritdoc />
        public virtual Folder CurrentDirectory { get; set; }

        /// <inheritdoc />
        public virtual Folder TemporaryFolder { get; set; }

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
        public virtual AbsolutePath ParsePathRelativeToWorkingDirectory(string path)
        {
            return TryParseAbsolutePath(path, CurrentDirectory).Value;
        }
    }
}