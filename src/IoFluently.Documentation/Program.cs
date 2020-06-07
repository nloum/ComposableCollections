using System;
using System.Linq;
using LiveLinq;
using ReactiveProcesses;

namespace IoFluently.Documentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new IoService(new ReactiveProcessFactory());
            var repositoryRoot = service.CurrentDirectory.Ancestors().First(ancestor => (ancestor / ".git").Exists());

            var markdownFiles = repositoryRoot.Descendants()
                .ToLiveLinq()
                .Where(x => x.HasExtension(".md") && x.GetPathType() == PathType.File)
                .Select(x => x.AsSmallTextFile());

            var htmls = markdownFiles
                .Select(markdownFile =>
                {
                    var markdown = markdownFile.Read();
                    var Html = Markdig.Markdown.ToHtml(markdown);
                    return new {markdownFile.Path, Html};
                });

            // var children = new Dictionary<AbsolutePath, Subject<ISetChange<AbsolutePath>>>();
            //
            //     htmls.Select(html =>
            // {
            //     
            //     markdownFiles.ToLiveLinq()
            //         .Where(x => x.Path.Parent() == )
            // })
            //
            // var foldersWithAReadMeFile = repositoryRoot.ToLiveLinq()
            //     .Where((path, type) => path.Name == "readme.md" && type == PathType.File)
            //     .KeysAsSet()
            //     .Select(path => path.Parent());
            //
            // var children = foldersWithAReadMeFile
            //     .GroupBy(path => path.Parent());
            //     // .Subscribe((parent, children) =>
            //     //     children.Subscribe(child => Console.WriteLine($"{parent} + {child}"), (child, _) => Console.WriteLine($"{parent} - {child}")),
            //     //     (parent, children, disposable) => disposable.Dispose());

            htmls
                .Subscribe(addedHtml => {
                    Console.WriteLine($"Adding a markdown file: {addedHtml.Path} with {0} children");
                    // foreach(var child in addedHtml.Children) {
                    //     Console.WriteLine($"- {child}");
                    // }
                    //addedHtml.Path.WithExtension(".html").WriteAllText(addedHtml.Html);
                }, (removedHtml, removalMode) => {
                    Console.WriteLine($"Removing a markdown file: {removedHtml.Path}");
                    //removedHtml.Path.WithExtension(".html").Delete();
                });
            
            // var ioService = new IoService(new ReactiveProcessFactory());
            //
            // var repoRoot = ioService.CurrentDirectory.Ancestors()
            //     .First(ancestor => (ancestor / "src" / "DocumentFluently.sln").Exists());
            //
            // var readmeMarkdownFile = (repoRoot / "readme.md").AsMarkdownFile();
            // var readmeHtmlFile = readmeMarkdownFile.Path.WithExtension(".html").AsHtmlFile();
            //
            // var markdown = readmeMarkdownFile.Read();
            // var html = markdown.ToHtml(x =>
            //     x.UseAdvancedExtensions()
            //         .UseDiagrams()
            //         .Build());
            // html = new Html(new[]{"<html><body><script src=\"https://cdn.jsdelivr.net/npm/mermaid@8.4.0/dist/mermaid.min.js\"></script><script>mermaid.initialize({startOnLoad:true,securityLevel:'loose'});</script>"}
            //     .Concat(html.Lines)
            //     .Concat(new[]{"</body></html>"}));
            // readmeHtmlFile.Write(html);
        }
    }
}