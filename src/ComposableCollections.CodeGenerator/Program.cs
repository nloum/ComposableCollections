using System;
using System.Collections.Generic;
using Autofac;
using CommandLine;

namespace ComposableCollections.CodeGenerator
{
	class Program
	{
		static void Main(string[] args)
        {
	        var parser = new Parser(with => with.EnableDashDash = true);
	        var parserResult = parser.ParseArguments<CommandLineOptions>(args);
	        Console.WriteLine(parserResult);
	        parserResult
		        .WithNotParsed(errors =>
		        {
			        foreach(var error in errors)
			        {
				        Console.WriteLine(error);
			        }
		        })
		        .WithParsed(options =>
		        {
			        var containerBuilder = new ContainerBuilder();
			        containerBuilder.RegisterModule<AutofacModule>();
			        var container = containerBuilder.Build();

			        var commandLineService = container.Resolve<ICommandLineService>();
			        commandLineService.Run(options);
		        });
        }
    }
}