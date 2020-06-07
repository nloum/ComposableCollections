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
    public static class Extensions
    {
        public static ImmutableHashSet<T> OtherwiseEmpty<T>(this IMaybe<ImmutableHashSet<T>> maybe)
        {
            return maybe.Otherwise(ImmutableHashSet<T>.Empty);
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var service = new IoService(new ReactiveProcessFactory());
            var repositoryRoot = service.CurrentDirectory.Ancestors().First(ancestor => (ancestor / ".git").Exists());
            var documentationRoot = repositoryRoot / "docs";

            var markdownFiles = documentationRoot.Descendants()
                .ToLiveLinq()
                .Where(x => x.HasExtension(".md") && x.GetPathType() == PathType.File)
                .Select(x => x.AsSmallTextFile());

            var htmls = markdownFiles
                .Select(markdownFile =>
                {
                    var markdown = markdownFile.Read();
                    var html = "<html>" + Markdig.Markdown.ToHtml(markdown) + "</html>";
                    var document = new XmlDocument();
                    document.LoadXml(html);
                    return new {markdownFile.Path, Html = document};
                });

            var backLinks = htmls.SelectMany(html =>
                {
                    var links = html.Html.SelectNodes("//a");
                    var backLinksForThisFile = new List<Tuple<AbsolutePath, AbsolutePath, string>>();
                    foreach (var link in links)
                    {
                        var linkEl = link as XmlElement;
                        var href = linkEl.Attributes["href"].InnerText;
                        var text = linkEl.InnerText;
                        if (href.Contains(":"))
                        {
                            continue;
                        }

                        var maybePath = service.TryParseRelativePath(href);
                        if (!maybePath.HasValue)
                        {
                            continue;
                        }

                        var linkTo = (html.Path.Parent() / maybePath.Value).Simplify();
                        backLinksForThisFile.Add(Tuple.Create(html.Path, linkTo, text));
                    }

                    return backLinksForThisFile.AsEnumerable();
                })
                .GroupBy(x => x.Item2)
                .SelectValue(x => x.Select(y => Tuple.Create(y.Item1, y.Item3)))
                .ToReadOnlyObservableDictionary();
            
            var transformedHtmls = htmls.Select(html => backLinks.ToLiveLinq()[html.Path]
                .SelectLatest(x =>
                {
                    return x.Select(y => y.ToObservableState()).Otherwise(() =>
                            Observable.Return(ImmutableHashSet<Tuple<AbsolutePath, string>>.Empty));
                })
                .Select(backLinksForThisHtml =>
            {
                var links = html.Html.SelectNodes("//a");
                foreach (var link in links)
                {
                    var linkEl = link as XmlElement;
                    var href = linkEl.Attributes["href"].InnerText;
                    var text = linkEl.InnerText;
                    if (href.Contains(":"))
                    {
                        continue;
                    }

                    var maybePath = service.TryParseRelativePath(href);
                    if (!maybePath.HasValue)
                    {
                        continue;
                    }

                    if (linkEl.Attributes["href"].InnerText.EndsWith(".md"))
                    {
                        linkEl.Attributes["href"].InnerText =
                            linkEl.Attributes["href"].InnerText
                                .Substring(0, linkEl.Attributes["href"].InnerText.Length - 3) + ".html";
                    }
                }
                
                var backLinkHtml =
                    "<ul>" + string.Join("\n",
                        backLinksForThisHtml.Select(x =>
                        {
                            var relativePath = x.Item1.WithExtension(".html").RelativeTo(documentationRoot);
                            return
                                    $"<li>This page is \"{x.Item2}\" of - <a href=\"{relativePath}\">{relativePath}</a></li>";
                        })) + "</ul>";
                return new {html.Path, Html = html.Html.OuterXml + backLinkHtml};
            }));
            
            transformedHtmls
                .Subscribe(addedHtml => {
                    Console.WriteLine($"Adding a markdown file: {addedHtml.Path}");
                    addedHtml.Path.WithExtension(".html").WriteAllText(addedHtml.Html);
                }, (removedHtml, removalMode) => {
                    Console.WriteLine($"Removing a markdown file: {removedHtml.Path}");
                    removedHtml.Path.WithExtension(".html").Delete();
                });
        }
    }
}