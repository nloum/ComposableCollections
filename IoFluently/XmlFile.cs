using System;
using System.Xml.Serialization;
using SimpleMonads;

namespace IoFluently
{
    public static class XmlFileExtensions
    {
        public static XmlFile ExpectXmlFile(this IHasAbsolutePath path)
        {
            return new XmlFile(path.Path);
        }

        public static XmlFileOrMissingPath ExpectXmlFileOrMissingPath(this IHasAbsolutePath path)
        {
            if (path.Path.IsFile)
            {
                return new XmlFileOrMissingPath(new XmlFile(path.Path));
            }
            
            return new XmlFileOrMissingPath(new MissingPath(path.Path));
        }
    }
    
    public class XmlFile : TextFile
    {
        public XmlFile(AbsolutePath path) : base(path)
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
    }

    public class XmlFileOrMissingPath : TextFileOrMissingPath
    {
        public XmlFileOrMissingPath(XmlFile item1) : base(item1)
        {
        }

        public XmlFileOrMissingPath(MissingPath item3) : base(item3)
        {
        }

        public XmlFileOrMissingPath(SubTypesOf<IHasAbsolutePath>.Either<File, MissingPath> other) : base(other)
        {
        }

        public XmlFileOrMissingPath(IHasAbsolutePath item) : base(item)
        {
        }

        public XmlFile Write<TModel>(TModel model)
        {
            var serializer = new XmlSerializer(typeof(TModel));
            using (var writer = OpenWriter())
            {
                serializer.Serialize(writer, model);
            }

            return new XmlFile(Path);
        }
    }
    
    public class XmlFile<TModel> : TextFile
    {
        public XmlFile(AbsolutePath path) : base(path)
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

        public XmlFileOrMissingPath(SubTypesOf<IHasAbsolutePath>.Either<File, MissingPath> other) : base(other)
        {
        }

        public XmlFileOrMissingPath(IHasAbsolutePath item) : base(item)
        {
        }

        public XmlFile Write(TModel model)
        {
            var serializer = new XmlSerializer(typeof(TModel));
            using (var writer = OpenWriter())
            {
                serializer.Serialize(writer, model);
            }

            return new XmlFile(Path);
        }
    }
}