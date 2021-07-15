namespace IoFluently.SystemTextJson
{
    public static class JsonFileExtensions
    {
        public static JsonFile ExpectJsonFile(this IFileOrFolderOrMissingPath path)
        {
            return new JsonFile(path);
        }

        public static JsonFileOrMissingPath ExpectJsonFileOrMissingPath(this IFileOrFolderOrMissingPath path)
        {
            if (path .IsFile)
            {
                return new JsonFileOrMissingPath(new JsonFile(path));
            }
            
            return new JsonFileOrMissingPath((IMissingPath)new MissingPath(path));
        }
    }
}