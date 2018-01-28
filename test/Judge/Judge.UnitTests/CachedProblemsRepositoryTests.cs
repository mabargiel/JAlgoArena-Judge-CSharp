using System.Collections.Generic;
using System.Linq;
using System.Text;
using Judge.Infrastructure.Data.Repositories;
using Judge.Infrastructure.Generators;
using Judge.Infrastructure.ProblemsSchema;
using NSubstitute;
using NSubstitute.Extensions;
using RestSharp;
using ServiceStack.Redis;
using Shouldly;
using Xbehave;
using Xunit;

namespace Judge.UnitTests
{
    public class CachedProblemsRepositoryTests
    {
        [Scenario]
        public void GetAllProblemsScenario(IRedisClient redisClient, IRestClient restClient,
            ISkeletonCodeGenerator skeletonCodeGenerator, CachedProblemsRepository repository, IEnumerable<Problem> problems)
        {
            "Given a Redis client with no problem cached"
                .x(() => redisClient = MockRedisClient());

            "And a REST api client"
                .x(() => restClient = MockProblemsApiClient());

            "And a skeleton code generator"
                .x(() => skeletonCodeGenerator = MockSkeletonCodeGenerator());

            "And the Repository"
                .x(() => repository = new CachedProblemsRepository(redisClient, restClient, skeletonCodeGenerator));

            "When I request all problems"
                .x(() => problems = repository.GetAll());

            "Then the problems api is called for problems meta data"
                .x(() => restClient.Received().Execute<List<Problem>>(Arg.Is<RestRequest>(request =>
                    request.Resource == "problems" && request.Method == Method.GET)));

            "And problems with skeleton code generated are cached by the redis client"
                .x(() => redisClient.Received().StoreAll(Arg.Is<List<Problem>>(list =>
                    list.All(problem => !string.IsNullOrEmpty(problem.SkeletonCode)))));

            "And the repository returns all problems"
                .x(() =>
                {
                    var problemsAsArray = problems as Problem[] ?? problems.ToArray();
                    problemsAsArray.Length.ShouldBe(2);
                });
        }

        private TestSkeletonCodeGenerator MockSkeletonCodeGenerator()
        {
            return new TestSkeletonCodeGenerator();
        }

        private class TestSkeletonCodeGenerator : ISkeletonCodeGenerator
        {
            public void GenerateFor(Problem problem)
            {
                problem.SkeletonCode = "Some C# string";
            }
        }

        private IRestClient MockProblemsApiClient()
        {
            var client = Substitute.For<IRestClient>();
            var testProblems = new List<Problem>
            {
                new Problem(null, null, null, 0, 0, null, null, 0),
                new Problem(null, null, null, 0, 0, null, null, 0)
            };
            client.Execute<List<Problem>>(Arg.Any<IRestRequest>()).Returns(new RestResponse<List<Problem>> {Data = testProblems});
            return client;
        }

        private static IRedisClient MockRedisClient()
        {
            return Substitute.For<IRedisClient>();
        }
    }
}
