using System.Text.Json;
using SimpleMonads;

namespace IoFluently.SystemTextJson
{
    public class JsonFileOrMissingPath : TextFileOrMissingPath
    {
        public JsonFileOrMissingPath(TextFile item1) : base(item1)
        {
        }

        public JsonFileOrMissingPath(IMissingPath item2) : base(item2)
        {
        }

        public JsonFileOrMissingPath(SubTypesOf<IFileOrFolderOrMissingPath>.Either<TextFile, IMissingPath> other) : base(other)
        {
        }

        public JsonFileOrMissingPath(IFileOrFolderOrMissingPath item) : base(item)
        {
        }

        public JsonFile Write<TModel>(TModel model)
        {
            this.WriteAllText(JsonSerializer.Serialize(model));
            return new JsonFile(this);
        }

        public JsonFile WriteJsonDocument(JsonDocument jsonDocument)
        {
            this.WriteAllText(jsonDocument.ToString());
            return new JsonFile(this);
        }
    }

    public class JsonFileOrMissingPath<TModel> : TextFileOrMissingPath
    {
        public JsonFileOrMissingPath(TextFile item1) : base(item1)
        {
        }

        public JsonFileOrMissingPath(IMissingPath item2) : base(item2)
        {
        }

        public JsonFileOrMissingPath(SubTypesOf<IFileOrFolderOrMissingPath>.Either<TextFile, IMissingPath> other) :
            base(other)
        {
        }

        public JsonFileOrMissingPath(IFileOrFolderOrMissingPath item) : base(item)
        {
        }

        public JsonFile Write(TModel model)
        {
            this.WriteAllText(JsonSerializer.Serialize(model));
            return new JsonFile(this);
        }
    }
}