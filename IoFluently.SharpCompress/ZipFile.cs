using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LiveLinq.Set;
using SharpCompress.Archives;
using SharpCompress.Archives.GZip;
using SharpCompress.Archives.Rar;
using SharpCompress.Archives.Tar;
using SharpCompress.Common;
using SharpCompress.Readers;
using SharpCompress.Readers.Zip;
using SharpCompress.Writers;
using UnitsNet;
using CompressionLevel = SharpCompress.Compressors.Deflate.CompressionLevel;

namespace IoFluently.SharpCompress
{
    public class ZipFile : File
    {
        private readonly string? _password;
    
        public ZipFile(IFileOrFolderOrMissingPath path, string? password = null) : base(path)
        {
            _password = password;
        }
    
        public ArchiveType ArchiveType => ArchiveType.Zip;
        
        public IReader Read(Information? bufferSize = null)
        {
            return ZipReader.Open(this.Open(FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: bufferSize), new ReaderOptions()
            {
                Password = _password,
                LeaveStreamOpen = false,
            });
        }
        
        public IWriter Write(CompressionType compressionType, string archiveComment,
            CompressionLevel deflateCompressionLevel,
            bool useZip64 = false,
            ArchiveEncoding archiveEncoding = null,
            Information? bufferSize = null)
        {
            return this.ExpectSharpCompressZipFileOrMissingPath()
                .Write(compressionType, archiveComment, deflateCompressionLevel,
                    useZip64, archiveEncoding, bufferSize);
        }
    }
}