using Judge.Infrastructure.Compile;
using Xunit;

namespace Judge.UnitTests
{
    public class InMemoryCSharpCompilerTests
    {
        private readonly string _sourceCode =
            "using System; public static class Solution {\npublic static string Greeting(string name) {\n\treturn \"Hello \" + name;\n}\n}\n";

        [Fact]
        public void CompileAndRunStaticMethod()
        {
            var assembly = new InMemoryCSharpCompiler().Compile(_sourceCode);
            var result = assembly.InvokeMethod("Solution", "Greeting", new object[] {"Julia"});

            Assert.Equal("Hello Julia", result);
        }

        [Fact]
        public void ThrowsCompilationExceptionWhenCouldNotCompile()
        {
            Assert.Throws<ProblemCompilationException>(() => new InMemoryCSharpCompiler().Compile(_sourceCode + "}}}"));
        }
    }
}