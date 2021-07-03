---
title: blah
---

# IoFluently

![dotnetcore](https://img.shields.io/github/workflow/status/nloum/IoFluently/dotnetcore) ![nuget](https://img.shields.io/nuget/v/IoFluently) ![license](https://img.shields.io/github/license/nloum/IoFluently) [![Join the chat at https://gitter.im/IoFluently/community](https://badges.gitter.im/IoFluently/community.svg)](https://gitter.im/IoFluently/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

IoFluently is a .NET Standard 2.0 class library for dealing with files and folders. The API is straightforward and dependency injectable.

Here's what it looks like:

```csharp(arg1 arg2 arg3)
var ioService = new IoService();
var readme = (ioService.CurrentDirectory / "docs" / "readme.md").txt;
#!html
    foreach(var line in readme.Lines()) {
        #!li
            line
        #!
    }
#!
var text = readme.Read();
Console.WriteLine(text);
```

|      | Man hours | Average labor rate | Total labor cost               |
|------|-----------|--------------------|--------------------------------|
| Type | Decimal   | USD                | Man hours * Average labor rate |
|      | 12        | $25.00             |                                |
|      | 13        | $25.00             |                                |
|      | 8         | $25.00             |                                |

Main advantages:

- I/O-heavy code is much more straightforward when it's written with IoFluently.
- I/O code is unit testable because the `IIoService` can be mocked or the in-memory implementation can be used (`InMemoryIoService`)
- I/O code can watch file system changes across platforms by polyfilling missing .NET features on non-Windows platforms
- Includes optional tracking of which files are open, for debugging purposes

## Major features

Not only does IoFluently allow the developer to write normal I/O code more fluently, it also provides major additional features for common I/O operations:

### Object-oriented file reading / writing

IoFluently provides an API for converting paths into strongly-typed objects that know how to read from and write to those paths.

Here is code that enumerates a directory (but not its subdirectories), finds all Markdown files, and converts those `AbsolutePath` objects into strongly-typed objects that know the path is a text file.

```c#
var markdownChildFiles = documentationFolder.Children()
    .Where(child => child.IsFile() && child.HasExtension(".md"))
    .Select(child => child.AsMarkdownFile());
```

This creates a lazy `IEnumerable` of `IPathWithKnownFormatSync<string>` objects, which can be used like this:

```c#
IPathWithKnownFormatSync<string> markdownFile = ...;
string markdown = markdownFile.Read();
markdown = markdown + "\n---\n";
markdownFile.Write(markdown);
```

Creating new file formats is easy:

```c#
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
var markdownFiles = documentationFolder.Descendants("*.md")
    .ToLiveLinq()
    .Subscribe(path =>
    {
        Console.WriteLine("A file was added: " + path);
    }, (path, _) =>
    {
        Console.WriteLine("A file was removed: " + path);
    });
```

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

### In-memory implementation

Any code that uses IoFluently requires minimal modification to run against an in-memory, virtual implementation of `IoService`. To do this, just use the `InMemoryIoService` class. For example:

```
IIoService ioService = new InMemoryIoService();
// any code that uses IIoService can now be backed by an in-memory implementation
```

### Acknowledgements

The line-splitting code is based on code by GÉRALD BARRÉ, also known as meziantou. Link: https://www.meziantou.net/split-a-string-into-lines-without-allocation.htm
</markdown>