using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Loader;
using Microsoft.Extensions.CommandLineUtils;

namespace ExecutionService
{
    class Program
    {
        static void Main()
        {
            var app = new CommandLineApplication();
            var compileAssembly = app.Command("execute", config =>
                {
                    config.Option("-cm|--classname", "class name", CommandOptionType.SingleValue);
                    config.Option("-mm|--methodname", "method name", CommandOptionType.SingleValue);
                    config.Option("-p|--dllpath", "DLL path", CommandOptionType.SingleValue);
                });
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.GetFullPath(args[0]));
            var result = assembly.GetType(args[1]).GetMethod(args[2]);
            Console.Out.Write(result);
        }
    }
}
