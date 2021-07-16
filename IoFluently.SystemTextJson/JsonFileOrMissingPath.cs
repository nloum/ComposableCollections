using System.Text.Json;
using SimpleMonads;

namespace IoFluently.SystemTextJson
{
    public class JsonFileOrMissingPath : TextFileOrMissingPath
    {
        public JsonFileOrMissingPath(IFileOrFolderOrMissingPath item) : base(item)
        {
        }

        public JsonFilePath Write<TModel>(TModel model)
        {
            this.WriteAllText(JsonSerializer.Serialize(model));
            return new JsonFilePath(this);
        }

        public JsonFilePath WriteJsonDocument(JsonDocument jsonDocument)
        {
            this.WriteAllText(jsonDocument.ToString());
            return new JsonFilePath(this);
        }
    }

    public class JsonFileOrMissingPath<TModel> : TextFileOrMissingPath
    {
        public JsonFileOrMissingPath(IFileOrFolderOrMissingPath item) : base(item)
        {
        }

        public JsonFilePath Write(TModel model)
        {
            this.WriteAllText(JsonSerializer.Serialize(model));
            return new JsonFilePath(this);
        }
    }
}