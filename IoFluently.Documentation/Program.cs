using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive.Linq;
using System.Xml;
using LiveLinq;
using ComposableCollections;

namespace IoFluently.Documentation
{
    public class CodeBlock
    {
        public string Settings { get; init; }
        public int StartingLineNumber { get; init; }
        public IReadOnlyList<string> LinesOfCode { get; init; }
        public bool ShowCode { get; init; }
        public bool ShowOutput { get; init; }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
    
    // public class LiterateDocument
    // {
    //     public AbsolutePath Path { get; init; }
    //     
    //     public IEnumerable<string> Weave()
    //     {
    //         foreach (var line in Path.ReadLines())
    //         {
    //             yield return line;
    //         }
    //     }
    //
    //     public IEnumerable<CodeBlock> GetCodeBlocks()
    //     {
    //         var isCurrentlyInTargetCodeBlock = false;
    //         string currentSettings = null;
    //         var lineNumber = 0;
    //         var startingLineNumber = 0;
    //         var linesOfCode = new List<string>();
    //
    //         foreach (var line in Path.ReadLines())
    //         {
    //             lineNumber++;
    //             if (line.Value.StartsWith("```"))
    //             {
    //                 if (isCurrentlyInTargetCodeBlock)
    //                 {
    //                     isCurrentlyInTargetCodeBlock = false;
    //                     yield return new CodeBlock()
    //                     {
    //                         Settings = currentSettings,
    //                         StartingLineNumber = startingLineNumber,
    //                         LinesOfCode = linesOfCode
    //                     };
    //                     continue;
    //                 }
    //                 else
    //                 {
    //                     isCurrentlyInTargetCodeBlock = true;
    //                     currentSettings = line.Value.TrimStart('`');
    //                     startingLineNumber = lineNumber;
    //                     continue;
    //                 }
    //             }
    //
    //             if (isCurrentlyInTargetCodeBlock)
    //             {
    //                 linesOfCode.Add(line);
    //             }
    //         }
    //     }
    // }
    
    class Program
    {
        static void Main(string[] args)
        {
            var ioService = new IoService();

            var repoRoot = ioService.CurrentDirectory.Ancestors.First(ancestor => ioService.IsFolder(ancestor / ".git"));
            var markdownFileRegex = ioService.FileNamePatternToRegex("*.md");
            
            var descendants = new AbsolutePathDescendants(repoRoot).ToLiveLinq()
                .AsObservable()
                .Publish()
                .RefCount()
                .ToLiveLinq();
            
            descendants.Where(path => markdownFileRegex.IsMatch(path))
                .Subscribe(path =>
                {
                    Console.WriteLine("File was added: " + path);
                }, (path, _) =>
                {
                    Console.WriteLine("File was removed: " + path);
                });

            Console.ReadKey();
            
            var xmlDoc = (repoRoot / "src/IoFluently/bin/Debug/IoFluently.xml").ExpectXmlFile();
            var csharpDocumentation = xmlDoc.ReadXmlDocument();

            var memberNodes = csharpDocumentation.SelectNodes("/doc/members/member").Cast<XmlElement>();
            
            var types = memberNodes.Where(memberNode => memberNode.Attributes["name"].Value[0] == 'T')
                //.Select(memberNode => memberNode.Attributes["name"].Value.Substring(2))
                .ToImmutableHashSet();

            foreach (var typeElement in types)
            {
                var typeName = typeElement.Attributes["name"].Value.Substring(2);
                var typeDocumentationFile = (repoRoot / "docs" / "src" / "docs" / "api" / $"{typeName}.mdx").ExpectTextFileOrMissingPath();    
                
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