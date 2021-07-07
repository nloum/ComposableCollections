# FluentSourceGenerators

This is a [.NET Source Generator](https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/) [NuGet package](https://www.nuget.org/packages/FluentSourceGenerators/) that makes it easier to build complex fluent APIs in C#.

## How to add it?

1. Add a dependency in your C# project on the latest version of `FluentSourceGenerators`.
2. Add a `FluentSourceGenerators.xml` file in your project. It needs to look like this:

```
<?xml version="1.0" encoding="utf-8" ?>
<FluentApiSourceGenerator>
  <CodeGenerators>
  </CodeGenerators>
</FluentApiSourceGenerator>
```

3. In the main `PropertyGroup` at the top of your `csproj` file, add this: `<AdditionalFileItemNames>FluentApiSourceGenerator.xml</AdditionalFileItemNames>`
4. Build your project

## What can it do?

There are several different code generators that can be configured in `FluentSourceGenerator.xml`. The sections below outline what they do and how to configure them. It is acceptable to have multiple of a single type of code generator. It's also possible to have a code generator that generates code off of code from a previous generator; in this just, know that the code generators are executed in the order they appear in `FluentSourceGenerator.xml`.

## AnonymousImplementationsGenerator

This generates anonymous implementations of interfaces. Here's an example of an anonymous implementation of the `IDisposable` interface:

```
public class AnonymousDisposable : IDisposable {
    private readonly Action _dispose;
	public AnonymousDisposable(Action dispose) {
	    _dispose = dispose;
	}
	
	public void Dispose() {
		_dispose();
	}
}
```

In other words, an anonymous implementation of an interface takes constructor parameters that are used to implement all the members of the interface.

The obvious problem here is that when you have an interface with tons of members, the constructor is going to have way too many parameters. The solution is that this `AnonymousImplementationsGenerator` lets you specify subtypes of the interface that can be passed in for delegate implementations. Here's an example:

```
public interface IBaseInterface {
	void Start();
    void Cancel();
	void Stop();
	bool IsRunning();
}
public interface ISubInterface {
	Task WaitUntilCompletion();
}
public class AnonymousImplementationOfSubInterface : ISubInterface {
    private readonly IBaseInterface _baseInterface;
	private readonly Func<Task> _waitUntilCompletion;
	
	public AnonymousImplementationOfSubInterface(IBaseInterface baseInterface, Func<Task> waitUntilCompletion) {
		_baseInterface = baseInterface;
		_waitUntilCompletion = waitUntilCompletion;
	}
	
	public void Start() {
		_baseInterface.Start();
	}
	
	public void Cancel() {
		_baseInterface.Cancel();
	}
	
	public void Stop() {
		_baseInterface.Stop();
	}
	
	public bool IsRunning() {
		return _baseInterface.IsRunning();
	}
	
	public Task WaitUntilCompletion() {
		return _waitUntilCompletion();
	}
}
```

In this example, by being able to base an `IBaseInterface` into the anonymous implementation's constructor, we reduce five constructor parameters to just two. This behavior is optional and completely configurable.

To configure this generator, put this in your `FluentApiSourceGenerator.xml`:

```
<?xml version="1.0" encoding="utf-8" ?>
<FluentApiSourceGenerator>
  <CodeGenerators>
    <AnonymousImplementations Namespace="LiveLinq.Dictionary.Anonymous">
      <AllowedArguments>
        <AllowedArgument>IComposableReadOnlyDictionary</AllowedArgument>
        <AllowedArgument>IComposableDictionary</AllowedArgument>
      </AllowedArguments>
      <InterfacesToImplement>
        <Interface>IObservableDictionary</Interface>
        <Interface>IObservableDisposableDictionary</Interface>
      </InterfacesToImplement>
    </AnonymousImplementations>
  </CodeGenerators>
</FluentApiSourceGenerator>
```
