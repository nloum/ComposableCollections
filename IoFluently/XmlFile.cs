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
            if (path.IsFile)
            {
                return new XmlFileOrMissingPath(new XmlFile(path));
            }
            
            return new XmlFileOrMissingPath(new MissingPath(path));
        }
    }
    
    public class XmlFile : TextFile
    {
        public XmlFile(FileOrFolderOrMissingPath path) : base(path)
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
            doc.Load(PleaseChangeMe.ToString());
            return doc;
        }
    }

    public class XmlFileOrMissingPath : TextFileOrMissingPath
    {
        public XmlFileOrMissingPath(XmlFile item1) : base(item1)
        {
        }

        public XmlFileOrMissingPath(MissingPath item3) : base(item3)
        {
        }

        public XmlFileOrMissingPath(SubTypesOf<IFileOrFolderOrMissingPath>.Either<File, MissingPath> other) : base(other)
        {
        }

        public XmlFileOrMissingPath(IFileOrFolderOrMissingPath item) : base(item)
        {
        }

        public XmlFile Write<TModel>(TModel model)
        {
            var serializer = new XmlSerializer(typeof(TModel));
            using (var writer = OpenWriter())
            {
                serializer.Serialize(writer, model);
            }

            return new XmlFile(PleaseChangeMe);
        }

        public XmlFile WriteXmlDocument(XmlDocument xmlDocument)
        {
            using (var stream = IoService.Open(this, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlDocument.Save(stream);
            }

            return new XmlFile(PleaseChangeMe);
        }
    }
    
    public class XmlFile<TModel> : TextFile
    {
        public XmlFile(FileOrFolderOrMissingPath path) : base(path)
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
        public XmlFileOrMissingPath(XmlFile<TModel> item1) : base(item1)
        {
        }

        public XmlFileOrMissingPath(MissingPath item3) : base(item3)
        {
        }

        public XmlFileOrMissingPath(SubTypesOf<IFileOrFolderOrMissingPath>.Either<File, MissingPath> other) : base(other)
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

            return new XmlFile(PleaseChangeMe);
        }
    }
}