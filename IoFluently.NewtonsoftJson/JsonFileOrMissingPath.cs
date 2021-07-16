using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleMonads;

namespace IoFluently.NewtonsoftJson
{
    public class JsonFileOrMissingPath : TextFileOrMissingPath
    {
        public JsonFileOrMissingPath(IFileOrFolderOrMissingPath item) : base(item)
        {
        }

        public JsonFilePath Write<TModel>(TModel model)
        {
            this.WriteAllText(JsonConvert.SerializeObject(model));
            return new JsonFilePath(this);
        }

        public JsonFilePath WriteJObject(JObject jObject)
        {
            this.WriteAllText(jObject.ToString());
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
            this.WriteAllText(JsonConvert.SerializeObject(model));
            return new JsonFilePath(this);
        }
    }
}