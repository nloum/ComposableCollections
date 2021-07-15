namespace IoFluently.SystemTextJson
{
    public static class JsonFileExtensions
    {
        public static JsonFilePath ExpectJsonFile(this IFileOrFolderOrMissingPath path)
        {
            return new JsonFilePath(path);
        }

        public static JsonFileOrMissingPath ExpectJsonFileOrMissingPath(this IFileOrFolderOrMissingPath path)
        {
            if (path .IsFile)
            {
                return new JsonFileOrMissingPath(new JsonFilePath(path));
            }
            
            return new JsonFileOrMissingPath((IMissingPath)new MissingPath(path));
        }

        public static JsonFileOrMissingPath ExpectMissingJsonFile(this IFileOrFolderOrMissingPath path)
        {
            return new JsonFileOrMissingPath((IMissingPath)new MissingPath(path));
        }
        
        public static JsonFilePath<TModel> ExpectJsonFile<TModel>(this IFileOrFolderOrMissingPath path)
        {
            return new JsonFilePath<TModel>(path);
        }

        public static JsonFileOrMissingPath<TModel> ExpectJsonFileOrMissingPath<TModel>(this IFileOrFolderOrMissingPath path)
        {
            if (path .IsFile)
            {
                return new JsonFileOrMissingPath<TModel>(new JsonFilePath(path));
            }
            
            return new JsonFileOrMissingPath<TModel>((IMissingPath)new MissingPath(path));
        }

        public static JsonFileOrMissingPath<TModel> ExpectMissingJsonFile<TModel>(this IFileOrFolderOrMissingPath path)
        {
            return new JsonFileOrMissingPath<TModel>((IMissingPath)new MissingPath(path));
        }
    }
}