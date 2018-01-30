using System.Collections.Generic;

namespace Judge.Infrastructure.ProblemsSchema
{

    //read only
    public class Problem
    {
        public string Id { get; }
        public string Title { get; }
        public string Description { get; }
        public int TimeLimit { get; }
        public int MemoryLimit { get; }
        public Function Function { get; }
        public List<TestCase> TestCases { get; }
        public string SkeletonCode { get; set; }
        public int Level { get; }

        public Problem (string id, string title, string description, int timeLimit,
            int memoryLimit, Function function, List<TestCase> testCases, int level)
        {
            Id = id;
            Title = title;
            Description = description;
            TimeLimit = timeLimit;
            MemoryLimit = memoryLimit;
            Function = function;
            TestCases = testCases;
            SkeletonCode = null;
            Level = level;
        }

        public Problem()
        {
            //For RestClient
        }
    }
}