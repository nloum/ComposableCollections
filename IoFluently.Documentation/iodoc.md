# IoDoc

IoDoc is a polyglot notebook format. A notebook can contain data, code, and HTML. This file is an example of an IoDoc file.

## Embedded code

```csharp name: myfirstblock, showOutput: false, showCode: true
Console.WriteLine("Hello world");
```

Output:

```csharp
myfirstblock.Output
```

Nested blocks are supported.

```csharp name: mysecondblock, showOutput: true, showCode: true
using System.Linq;

foreach(var i in Enumerable.Repeat(1, 10)) {
   #!markdown
      Hello world `i`
   #!
}

```

Blocks can start with standard Markdown syntax (` ``` `) or with a shebang (`#!`) followed by the language that the block will be in. The IoDoc interpreter converts these nested blocks into string literals with interpolation. This feature is supported for many different languages:

| Task                            |
|---------------------------------|
| Add more language examples here |

## Embedded data

IoDoc also supports embedding data in the file. Code blocks can expose data like this:

```csharp name: myfirstblock, showOutput: false, showCode: true
Console.WriteLine("Hello world");
```

Output:

Format:

```csharp
myfirstblock.Output.Format
```

Value:

```csharp
myfirstblock.Output.Value
```

Code blocks can also be in CSV format:

```csv
Task,Due
Add a better CSV format here,before release
```

Data can also be embedded in the form of a table:

|      | Man hours | Average labor rate | Total labor cost               |
|------|-----------|--------------------|--------------------------------|
| Type | Decimal   | USD                | Man hours * Average labor rate |
|      | 12        | $25.00             |                                |
|      | 13        | $25.00             | $500.00                        |
|      | 8         | $25.00             |                                |

Note that there is a computed column in the above table. Also note that units like `$` are supported.

This data is then exposed via auto-generated, strongly typed GraphQL APIs to the code that is embedded in the file. Note that code can't access data that is embedded beneath it.