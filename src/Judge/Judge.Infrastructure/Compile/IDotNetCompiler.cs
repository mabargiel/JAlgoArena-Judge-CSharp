namespace Judge.Infrastructure.Compile
{
    public interface IDotNetCompiler
    {
        CompiledAssembly Compile(string code);
    }
}