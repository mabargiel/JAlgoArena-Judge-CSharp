using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Judge.Infrastructure.Compile
{
    public class InMemoryCSharpCompiler : IDotNetCompiler
    {
        public CompiledAssembly Compile(string code)
        {
            var compilation = CSharpCompilation.Create(Guid.NewGuid().ToString())
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(
                    MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location))
                .AddSyntaxTrees(CSharpSyntaxTree.ParseText(code));

            using (var ms = new MemoryStream())
            {
                var result = compilation.Emit(ms);

                if (!result.Success)
                {
                    var failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error).ToArray();

                    throw new ProblemCompilationException("Compilation Failed", failures.ToImmutableArray());
                }

                ms.Seek(0, SeekOrigin.Begin);
                var assembly = Assembly.Load((byte[]) ms.ToArray());
                return new CompiledAssembly(assembly);
            }
        }
    }
}