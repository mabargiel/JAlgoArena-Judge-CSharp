using Judge.Infrastructure.ProblemsSchema;

namespace Judge.Infrastructure.Generators
{
    public interface ISkeletonCodeGenerator
    {
        void GenerateFor(Problem problem);
    }
}