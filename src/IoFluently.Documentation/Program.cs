using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive.Linq;
using System.Xml;
using LiveLinq;
using ReactiveProcesses;
using SimpleMonads;

namespace IoFluently.Documentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var ioService = new IoService(new ReactiveProcessFactory());

            var repoRoot = ioService.CurrentDirectory.Ancestors().First(ancestor => ioService.IsFolder(ancestor / ".git"));
            var xmlDoc = (repoRoot / "src/IoFluently/bin/Debug/IoFluently.xml").AsXmlFile();
            var csharpDocumentation = xmlDoc.Read();

            var memberNodes = csharpDocumentation.SelectNodes("/doc/members/member").Cast<XmlElement>();
            
            var types = memberNodes.Where(memberNode => memberNode.Attributes["name"].Value[0] == 'T')
                //.Select(memberNode => memberNode.Attributes["name"].Value.Substring(2))
                .ToImmutableHashSet();

            foreach (var typeElement in types)
            {
                var typeName = typeElement.Attributes["name"].Value.Substring(2);
                var typeDocumentationFile = repoRoot / "docs" / "src" / "docs" / "api" / $"{typeName}.mdx";    
                
                using (var writer = typeDocumentationFile.OpenWriter())
                {
                    writer.WriteLine("---");
                    writer.WriteLine($"title: '{typeName}'");
                    writer.WriteLine("---\n");
                
                    foreach (var memberNode in memberNodes)
                    {
                        var name = (XmlAttribute)memberNode.SelectSingleNode("@name");
                        if (!name.Value.Substring(2).StartsWith(typeName + "."))
                        {
                            continue;
                        }
                    
                        var summary = (XmlElement)memberNode.SelectSingleNode("summary");
                        var parameters = memberNode.SelectNodes("param").Cast<XmlElement>();
                        var returns = (XmlElement) memberNode.SelectSingleNode("returns");

                        writer.WriteLine($"## {name.Value.Substring(3 + typeName.Length)}\n");
                        writer.WriteLine(string.Join("\n", summary.InnerXml.Split("\n").Select(line => line.Trim())));
                    }
                }
            }
        }
    }
}