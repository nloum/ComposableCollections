using System.Text.Json;

namespace IoFluently.SystemTextJson
{
    public class JsonFile : TextFile
    {
        public JsonFile(IFileOrFolderOrMissingPath path) : base(path)
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
    
    public class JsonFile<TModel> : TextFile
    {
        public JsonFile(IFileOrFolderOrMissingPath path) : base(path)
        {
        }

        public TModel Read()
        {
            return JsonSerializer.Deserialize<TModel>(this.ReadAllText());
        }
    }
}