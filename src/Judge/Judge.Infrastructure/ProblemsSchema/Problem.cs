using System.Collections.Generic;

namespace Judge.Infrastructure.ProblemsSchema
{
    //read only
    public class Problem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TimeLimit { get; set; }
        public int MemoryLimit { get; set; }
        public Function Function { get; set; }
        public List<TestCase> TestCases { get; set; }
        public Dictionary<string, string> SkeletonCode { get; set; }
        public int Level { get; set; }
    }
}