using Judge.Infrastructure.ProblemsSchema;

namespace Judge.Infrastructure.LangGenerators
{
    /// <summary>
    ///     Generates base code in specified language (depends on implementation)
    /// </summary>
    public interface ISkeletonCodeGenerator
    {
        string Generate(Function function);
    }
}