using System.Text.Json;

namespace IoFluently.SystemTextJson
{
    public class JsonFilePath : TextFilePath
    {
        public JsonFilePath(IFileOrFolderOrMissingPath path) : base(path)
        {
        }

        public TModel Read<TModel>()
        {
            return JsonSerializer.Deserialize<TModel>(this.ReadAllText());
        }
        
        public JsonDocument ReadJsonDocument()
        {
            return JsonDocument.Parse(this.ReadAllText());
        }
    }
    
    public class JsonFilePath<TModel> : TextFilePath
    {
        public JsonFilePath(IFileOrFolderOrMissingPath path) : base(path)
        {
        }

        public TModel Read()
        {
            return JsonSerializer.Deserialize<TModel>(this.ReadAllText());
        }
    }
}