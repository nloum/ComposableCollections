using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using SimpleMonads;

namespace IoFluently
{
    public static class XmlFileExtensions
    {
        public static XmlFile ExpectXmlFile(this IFileOrFolderOrMissingPath path)
        {
            return new XmlFile(path);
        }

        public static XmlFileOrMissingPath ExpectXmlFileOrMissingPath(this IFileOrFolderOrMissingPath path)
        {
            if (path.Path.IsFile)
            {
                return new XmlFileOrMissingPath(new XmlFile(path));
            }
            
            return new XmlFileOrMissingPath((IMissingPath)new MissingPath(path));
        }
    }
    
    public class XmlFile : TextFile
    {
        public XmlFile(IFileOrFolderOrMissingPath path) : base(path)
        {
        }

        public TModel Read<TModel>()
        {
            var serializer = new XmlSerializer(typeof(TModel));
            using (var reader = OpenReader())
            {
                var result = (TModel) serializer.Deserialize(reader);
                return result;
            }
        }
        
        public XmlDocument ReadXmlDocument()
        {
            var doc = new XmlDocument();
            doc.Load(this.ToString());
            return doc;
        }
    }

    public class XmlFileOrMissingPath : TextFileOrMissingPath
    {
        public XmlFileOrMissingPath(TextFile item1) : base(item1)
        {
        }

        public XmlFileOrMissingPath(IMissingPath item2) : base(item2)
        {
        }

        public XmlFileOrMissingPath(SubTypesOf<IFileOrFolderOrMissingPath>.Either<TextFile, IMissingPath> other) : base(other)
        {
        }

        public XmlFileOrMissingPath(IFileOrFolderOrMissingPath item) : base(item)
        {
        }

        public XmlFile Write<TModel>(TModel model)
        {
            var serializer = new XmlSerializer(typeof(TModel));
            using (var writer = this.OpenWriter())
            {
                serializer.Serialize(writer, model);
            }

            return new XmlFile(this);
        }

        public XmlFile WriteXmlDocument(XmlDocument xmlDocument)
        {
            using (var stream = this.Open(FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlDocument.Save(stream);
            }

            return new XmlFile(this);
        }
    }
    
    public class XmlFile<TModel> : TextFile
    {
        public XmlFile(IFileOrFolderOrMissingPath path) : base(path)
        {
        }

        public TModel Read()
        {
            var serializer = new XmlSerializer(typeof(TModel));
            using (var reader = OpenReader())
            {
                var result = (TModel) serializer.Deserialize(reader);
                return result;
            }
        }
    }

    public class XmlFileOrMissingPath<TModel> : TextFileOrMissingPath
    {
        public XmlFileOrMissingPath(TextFile item1) : base(item1)
        {
        }

        public XmlFileOrMissingPath(IMissingPath item2) : base(item2)
        {
        }

        public XmlFileOrMissingPath(SubTypesOf<IFileOrFolderOrMissingPath>.Either<TextFile, IMissingPath> other) : base(other)
        {
        }

        public XmlFileOrMissingPath(IFileOrFolderOrMissingPath item) : base(item)
        {
        }

        public XmlFile Write(TModel model)
        {
            var serializer = new XmlSerializer(typeof(TModel));
            using (var writer = OpenWriter())
            {
                serializer.Serialize(writer, model);
            }

            return new XmlFile(this);
        }
    }
}