namespace IoFluently.SharpCompress
{
    public static class Extensions
    {
        public static RarFilePath ExpectRarFile(this IFileOrFolderOrMissingPath path)
        {
            return new RarFilePath(path);
        }
        
        public static SevenZipFilePath Expect7ZipFile(this IFileOrFolderOrMissingPath path)
        {
            return new SevenZipFilePath(path);
        }
        
        public static TarFilePath ExpectTarFile(this IFileOrFolderOrMissingPath path)
        {
            return new TarFilePath(path);
        }
        
        public static TarFileOrMissingPath ExpectTarFileOrMissingPath(this IFileOrFolderOrMissingPath path)
        {
            return new TarFileOrMissingPath(path);
        }
        
        public static GZipFilePath ExpectGZipFile(this IFileOrFolderOrMissingPath path)
        {
            return new GZipFilePath(path);
        }
        
        public static GZipFileOrMissingPath ExpectGZipFileOrMissingPath(this IFileOrFolderOrMissingPath path)
        {
            return new GZipFileOrMissingPath(path);
        }
        
        public static ZipFilePath ExpectSharpCompressZipFile(this IFileOrFolderOrMissingPath path)
        {
            return new ZipFilePath(path);
        }
        
        public static ZipFileOrMissingPath ExpectSharpCompressZipFileOrMissingPath(this IFileOrFolderOrMissingPath path)
        {
            return new ZipFileOrMissingPath(path);
        }
    }
}