using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using SimpleMonads;

namespace IoFluently
{
    public static class XmlFileExtensions
    {
        public static XmlFilePath ExpectXmlFile(this IFileOrFolderOrMissingPath path)
        {
            return new XmlFilePath(path);
        }

        public static XmlFileOrMissingPath ExpectXmlFileOrMissingPath(this IFileOrFolderOrMissingPath path)
        {
            if (path .IsFile)
            {
                return new XmlFileOrMissingPath(new XmlFilePath(path));
            }
            
            return new XmlFileOrMissingPath((IMissingPath)new MissingPath(path));
        }
        
        public static XmlFileOrMissingPath ExpectMissingXmlFile(this IFileOrFolderOrMissingPath path)
        {
            return new XmlFileOrMissingPath((IMissingPath)new MissingPath(path));
        }
        
        public static XmlFilePath<TModel> ExpectXmlFile<TModel>(this IFileOrFolderOrMissingPath path)
        {
            return new XmlFilePath<TModel>(path);
        }

        public static XmlFileOrMissingPath<TModel> ExpectXmlFileOrMissingPath<TModel>(this IFileOrFolderOrMissingPath path)
        {
            if (path .IsFile)
            {
                return new XmlFileOrMissingPath<TModel>(new XmlFilePath<TModel>(path));
            }
            
            return new XmlFileOrMissingPath<TModel>((IMissingPath)new MissingPath(path));
        }
        
        public static XmlFileOrMissingPath<TModel> ExpectMissingXmlFile<TModel>(this IFileOrFolderOrMissingPath path)
        {
            return new XmlFileOrMissingPath<TModel>((IMissingPath)new MissingPath(path));
        }
    }
    
    public class XmlFilePath : TextFilePath
    {
        public XmlFilePath(IFileOrFolderOrMissingPath path) : base(path)
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
            doc.Load(FullName);
            return doc;
        }
    }

    public class XmlFileOrMissingPath : TextFileOrMissingPath
    {
        public XmlFileOrMissingPath(TextFilePath item1) : base(item1)
        {
        }

        public XmlFileOrMissingPath(IMissingPath item2) : base(item2)
        {
        }

        public XmlFileOrMissingPath(SubTypesOf<IFileOrFolderOrMissingPath>.Either<TextFilePath, IMissingPath> other) : base(other)
        {
        }

        public XmlFileOrMissingPath(IFileOrFolderOrMissingPath item) : base(item)
        {
        }

        public XmlFilePath Write<TModel>(TModel model)
        {
            var serializer = new XmlSerializer(typeof(TModel));
            using (var writer = this.OpenWriter())
            {
                serializer.Serialize(writer, model);
            }

            return new XmlFilePath(this);
        }

        public XmlFilePath WriteXmlDocument(XmlDocument xmlDocument)
        {
            using (var stream = this.Open(FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlDocument.Save(stream);
            }

            return new XmlFilePath(this);
        }
    }
    
    public class XmlFilePath<TModel> : TextFilePath
    {
        public XmlFilePath(IFileOrFolderOrMissingPath path) : base(path)
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
        public XmlFileOrMissingPath(TextFilePath item1) : base(item1)
        {
        }

        public XmlFileOrMissingPath(IMissingPath item2) : base(item2)
        {
        }

        public XmlFileOrMissingPath(SubTypesOf<IFileOrFolderOrMissingPath>.Either<TextFilePath, IMissingPath> other) : base(other)
        {
        }

        public XmlFileOrMissingPath(IFileOrFolderOrMissingPath item) : base(item)
        {
        }

        public XmlFilePath Write(TModel model)
        {
            var serializer = new XmlSerializer(typeof(TModel));
            using (var writer = OpenWriter())
            {
                serializer.Serialize(writer, model);
            }

            return new XmlFilePath(this);
        }
    }
}