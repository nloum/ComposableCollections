using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IoFluently.NewtonsoftJson
{
    public class JsonFilePath : TextFilePath
    {
        public JsonFilePath(IFileOrFolderOrMissingPath path) : base(path)
        {
        }

        public TModel Read<TModel>()
        {
            return JsonConvert.DeserializeObject<TModel>(this.ReadAllText());
        }
        
        public JObject ReadJObject()
        {
            return JObject.Parse(this.ReadAllText());
        }
    }

    public class JsonFilePath<TModel> : TextFilePath
    {
        public JsonFilePath(IFileOrFolderOrMissingPath path) : base(path)
        {
        }

        public TModel Read()
        {
            return JsonConvert.DeserializeObject<TModel>(this.ReadAllText());
        }
    }
}