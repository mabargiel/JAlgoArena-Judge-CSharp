using Judge.Infrastructure.ProblemsSchema;

namespace Judge.API.Models
{
    public class ProblemDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TimeLimit { get; set; }
        public int MemoryLimit { get; set; }
        public Function Function { get; set; }
        public TestCase[] TestCases { get; set; }
        public string SkeletonCode { get; set; }
        public int Level { get; set; }
    }
}