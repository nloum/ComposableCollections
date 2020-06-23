# IoFluently

IoFluently is a .NET Standard 2.0 class library for dealing with files and folders. Its API is fluent, mockable, and dependency injectable.

Here's what it looks like:

```
// In dependeny-injectable code, ioService would be dependency-injected for easy unit testing
var ioService = new IoService(new ReactiveProcessFactory());
var readme = (ioService.CurrentDirectory / "docs" / "readme.md").AsSmallTextFile();
var text = readme.Read();
Console.WriteLine(text);
```

I/O-heavy code is much more fluent when it's written with IoFluently.

## Major features

Not only does IoFluently allow the developer to write normal I/O code more fluently, it also provides major additional features for common I/O operations:

### Object-oriented file reading / writing

IoFluently provides an API for converting paths into strongly-typed objects that know how to read from and write to those paths.

Here is code that enumerates a directory (but not its subdirectories), finds all Markdown files, and converts those `AbsolutePath` objects into strongly-typed objects that know the path is a text file.

```
var markdownChildFiles = documentationFolder.Children()
    .Where(child => child.IsFile() && child.HasExtension(".md"))
    .Select(child => child.AsMarkdownFile());
```

This creates a lazy `IEnumerable` of `IPathWithKnownFormatSync<string>` objects, which can be used like this:

```
IPathWithKnownFormatSync<string> markdownFile = ...;
string markdown = markdownFile.Read();
markdown = markdown + "\n---\n";
markdownFile.Write(markdown);
```

Creating new file formats is easy:

```
public static class Extensions
{
    public static IPathWithKnownFormatSync<string> AsMarkdownFile(this AbsolutePath path)
    {
        return path.AsPathFormat(absPath => absPath.ReadAllText(), (absPath, text) => absPath.WriteAllText(text));
    }
}
```

There are `Task`-oriented variations of this API that work asynchronously.

### Monitoring for filesystem changes

IoFluently provides a powerful API for monitoring files and folders for changes. This API works seamlessly across Mac OS, Windows, and Linux.

Here is code that efficiently creates an observable set of Markdown files:

```
var markdownFiles = documentationFolder.Descendants()
    .ToLiveLinq()
    .Where(x => x.HasExtension(".md") && x.GetPathType() == PathType.File);
```

The lambda in the `.Where` clause gets called once for each new file that is added to the path `documentationFolder` or one of its subdirectories.

### Powerful copy/move APIs

IoFluently makes it easy to copy or move files in a very natural way. Here's an example of a program that copies everything from the `packages` folder to the `new-packages` folder (but not if there's a more recent version of a file in the destination):

```
var repositoryRoot = service.CurrentDirectory.Ancestors().First(ancestor => (ancestor / ".git").Exists());

var source = repositoryRoot / "packages";
var destination = repositoryRoot / "new-packages";

foreach (var sourceAndDestination in source.Translate(destination))
{
    // Each sourceAndDestination represents a single source / destination pair.
    if (sourceAndDestination.Source.LastWriteTime() > sourceAndDestination.Destination.LastWriteTime())
    {
        sourceAndDestination.Copy(overwrite: true);
    }
}
```

Because the copy operation consists of a loop, you can easily change the behavior depending on arbitrary behaviors that the developer can define.

