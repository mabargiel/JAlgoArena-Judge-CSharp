using System.IO;
using System.Threading.Tasks;
using Judge.Infrastructure.Models;
using Judge.Infrastructure.ProblemsSchema;
using Newtonsoft.Json;
using Xbehave;
using Xunit;

namespace Judge.UnitTests
{
    public class JudgeTests
    {
        [Scenario]
        public async Task
            JudgeBinarySearchScenario(
                string binarySearchCode,
                int timeLimit,
                int memoryLimit,
                JudgeService judge,
                JudgeResult result,
                TestCase[] testCases)
        {
            "Given a problem"
                .x(() => binarySearchCode = LoadBinarySearchCode());

            "And time limit"
                .x(() => timeLimit = 1);

            "And memory limit"
                .x(() => memoryLimit = 32);

            "And 2 test cases"
                .x(() => testCases = new[]
                {
                    new TestCase
                    {
                        Input = new [] {1, 2, 3}
                    },
                    new TestCase()
                });

            "And the judge"
                .x(() => judge = new JudgeService());

            "When I hit 'run' button"
                .x(() => result = judge.Run(binarySearchCode, timeLimit, memoryLimit, testCases));

            "Then the code is run agains all test cases"
                .x(() => Assert.Equal(new[] {true, false}, result.TestcaseResults));
        }

        private string LoadBinarySearchCode()
        {
            return File.ReadAllText("Resources/BinarySearch.cs");
        }
    }

    public class JudgeService
    {
        public JudgeResult Run(string code, int timeLimit, int memoryLimit, TestCase[] testCases)
        {
            
        }
    }
}