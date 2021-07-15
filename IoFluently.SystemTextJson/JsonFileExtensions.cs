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

        public static JsonFileOrMissingPath ExpectMissingJsonFile(this IFileOrFolderOrMissingPath path)
        {
            return new JsonFileOrMissingPath((IMissingPath)new MissingPath(path));
        }
        
        public static JsonFile<TModel> ExpectJsonFile<TModel>(this IFileOrFolderOrMissingPath path)
        {
            return new JsonFile<TModel>(path);
        }

        public static JsonFileOrMissingPath<TModel> ExpectJsonFileOrMissingPath<TModel>(this IFileOrFolderOrMissingPath path)
        {
            if (path .IsFile)
            {
                return new JsonFileOrMissingPath<TModel>(new JsonFile(path));
            }
            
            return new JsonFileOrMissingPath<TModel>((IMissingPath)new MissingPath(path));
        }

        public static JsonFileOrMissingPath<TModel> ExpectMissingJsonFile<TModel>(this IFileOrFolderOrMissingPath path)
        {
            return new JsonFileOrMissingPath<TModel>((IMissingPath)new MissingPath(path));
        }
    }
}