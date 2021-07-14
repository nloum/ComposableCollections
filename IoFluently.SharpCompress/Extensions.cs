namespace IoFluently.SharpCompress
{
    public static class Extensions
    {
        public static RarFile ExpectRarFile(this IFileOrFolderOrMissingPath path)
        {
            return new RarFile(path);
        }
        
        public static SevenZipFile Expect7ZipFile(this IFileOrFolderOrMissingPath path)
        {
            return new SevenZipFile(path);
        }
        
        public static TarFile ExpectTarFile(this IFileOrFolderOrMissingPath path)
        {
            return new TarFile(path);
        }
        
        public static TarFileOrMissingPath ExpectTarFileOrMissingPath(this IFileOrFolderOrMissingPath path)
        {
            return new TarFileOrMissingPath(path);
        }
        
        public static GZipFile ExpectGZipFile(this IFileOrFolderOrMissingPath path)
        {
            return new GZipFile(path);
        }
        
        public static GZipFileOrMissingPath ExpectGZipFileOrMissingPath(this IFileOrFolderOrMissingPath path)
        {
            return new GZipFileOrMissingPath(path);
        }
        
        public static ZipFile ExpectSharpCompressZipFile(this IFileOrFolderOrMissingPath path)
        {
            return new ZipFile(path);
        }
        
        public static ZipFileOrMissingPath ExpectSharpCompressZipFileOrMissingPath(this IFileOrFolderOrMissingPath path)
        {
            return new ZipFileOrMissingPath(path);
        }
    }
}