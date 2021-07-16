namespace IoFluently.NewtonsoftJson
{
    public static class JsonFileExtensions
    {
        public static JsonFilePath ExpectJsonFile(this IFileOrFolderOrMissingPath path)
        {
            return new(path);
        }

        public static JsonFileOrMissingPath ExpectJsonFileOrMissingPath(this IFileOrFolderOrMissingPath path)
        {
            return new(path);
        }
        
        public static JsonFileOrMissingPath ExpectMissingJsonFile(this IFileOrFolderOrMissingPath path)
        {
            return new(path);
        }
        
        public static JsonFilePath<TModel> ExpectJsonFile<TModel>(this IFileOrFolderOrMissingPath path)
        {
            return new(path);
        }

        public static JsonFileOrMissingPath<TModel> ExpectJsonFileOrMissingPath<TModel>(this IFileOrFolderOrMissingPath path)
        {
            return new(path);
        }

        public static JsonFileOrMissingPath<TModel> ExpectMissingJsonFile<TModel>(this IFileOrFolderOrMissingPath path)
        {
            return new(path);
        }
    }
}