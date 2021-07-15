using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleMonads;

namespace IoFluently.NewtonsoftJson
{
    public class JsonFileOrMissingPath : TextFileOrMissingPath
    {
        public JsonFileOrMissingPath(TextFilePath item1) : base(item1)
        {
        }

        public JsonFileOrMissingPath(IMissingPath item2) : base(item2)
        {
        }

        public JsonFileOrMissingPath(SubTypesOf<IFileOrFolderOrMissingPath>.Either<TextFilePath, IMissingPath> other) : base(other)
        {
        }

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
        public JsonFileOrMissingPath(TextFilePath item1) : base(item1)
        {
        }

        public JsonFileOrMissingPath(IMissingPath item2) : base(item2)
        {
        }

        public JsonFileOrMissingPath(SubTypesOf<IFileOrFolderOrMissingPath>.Either<TextFilePath, IMissingPath> other) :
            base(other)
        {
        }

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