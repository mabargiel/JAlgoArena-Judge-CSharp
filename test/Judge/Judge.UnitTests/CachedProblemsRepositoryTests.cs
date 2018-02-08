using System.Collections.Generic;
using System.Linq;
using Judge.Infrastructure.Data.Repositories;
using Judge.Infrastructure.LangGenerators;
using Judge.Infrastructure.ProblemsSchema;
using NSubstitute;
using RestSharp;
using ServiceStack.Redis;
using Shouldly;
using Xbehave;

namespace Judge.UnitTests
{
    public class CachedProblemsRepositoryTests
    {
        [Scenario]
        public void GetAllProblemsScenario(IRedisClient redisClient, IRestClient restClient,
            ISkeletonCodeGenerator skeletonCodeGenerator, IProblemsRepository repository,
            IEnumerable<Problem> problems)
        {
            "Given a Redis client"
                .x(() => redisClient = MockRedisClient());

            "And a REST api client"
                .x(() => restClient = MockProblemsApiClient());

            "And a skeleton code generator"
                .x(() => skeletonCodeGenerator = MockSkeletonCodeGenerator());

            "And the Repository"
                .x(() => repository = new CachedProblemsRepository(redisClient, restClient, skeletonCodeGenerator));

            "When I request all problems from the repository"
                .x(async () => problems = await repository.GetAllAsync());

            "Then problem meta datas are requested using api"
                .x(() => restClient.Received().Execute<List<Problem>>(Arg.Is<RestRequest>(request =>
                    request.Resource == "problems" && request.Method == Method.GET)));

            "And problems with skeleton code generated are cached by the redis client"
                .x(() => redisClient.Received().StoreAll(Arg.Is<List<Problem>>(list =>
                    list.All(problem => problem.SkeletonCode.ContainsKey("C#")))));

            "And the repository returns all problems"
                .x(() =>
                {
                    var problemsAsArray = problems as Problem[] ?? problems.ToArray();
                    problemsAsArray.Length.ShouldBe(2);
                    problemsAsArray.ShouldAllBe(problem => problem.SkeletonCode.ContainsKey("C#"));
                });
        }

        [Scenario]
        public void GetProblemById_WhenIsNotCachedYet(IRedisClient redisClient, IRestClient restClient,
            ISkeletonCodeGenerator skeletonCodeGenerator, IProblemsRepository repository,
            Problem problem)
        {
            "Given a Redis client"
                .x(() => redisClient = MockRedisClient());

            "And a REST api client"
                .x(() =>
                {
                    restClient = MockProblemsApiClient();
                    restClient.Execute<Problem>(Arg.Any<RestRequest>()).Returns(
                        new RestResponse<Problem> {Data = new Problem {Id = "problemId"}});
                });

            "And a skeleton code generator"
                .x(() => skeletonCodeGenerator = MockSkeletonCodeGenerator());

            "And the Repository"
                .x(() => repository = new CachedProblemsRepository(redisClient, restClient, skeletonCodeGenerator));

            "When I request a problem from the repository"
                .x(async () => problem = await repository.FindByIdAsync("problemId"));

            "Then the problem meta data is requested using api"
                .x(() => restClient.Received().Execute<Problem>(Arg.Is<RestRequest>(request =>
                    request.Resource == "problems/problemId" && request.Method == Method.GET)));

            "And the problem is cached"
                .x(() => redisClient.Received()
                    .Store(Arg.Is<Problem>(p => p != null && p.Id == "problemId")));

            "And the repository returns the problem"
                .x(() =>
                {
                    problem.ShouldNotBe(null);
                    problem.Id.ShouldBe("problemId");
                    problem.SkeletonCode.ShouldNotBeNull();
                });
        }

        [Scenario]
        public void GetProblemById_IsAlreadyCached(IRedisClient redisClient, IRestClient restClient,
            ISkeletonCodeGenerator skeletonCodeGenerator, IProblemsRepository repository,
            Problem problem)
        {
            "Given a Redis client with a cached problem"
                .x(() =>
                {
                    redisClient = MockRedisClient();
                    redisClient.GetById<Problem>(Arg.Is<string>(o => o == "problemId"))
                        .Returns(info =>
                        {
                            var p = new Problem
                            {
                                Id = "problemId",
                                SkeletonCode = new Dictionary<string, string> {{"C#", "Some C# Code"}}
                            };
                            return p;
                        });
                });

            "And a REST api client"
                .x(() => restClient = MockProblemsApiClient());

            "And a skeleton code generator"
                .x(() => skeletonCodeGenerator = MockSkeletonCodeGenerator());

            "And the Repository"
                .x(() => repository = new CachedProblemsRepository(redisClient, restClient, skeletonCodeGenerator));

            "When I request a problem from the repository"
                .x(async () => problem = await repository.FindByIdAsync("problemId"));

            "Then the problem meta data is NOT requested using api"
                .x(() => restClient.DidNotReceive().Execute<Problem>(Arg.Any<RestRequest>()));

            "And the repository returns the problem with generated skeletonCode"
                .x(() =>
                {
                    problem.ShouldNotBe(null);
                    problem.Id.ShouldBe("problemId");
                    problem.SkeletonCode.ShouldNotBeNull();
                });
        }

        private TestSkeletonCodeGenerator MockSkeletonCodeGenerator()
        {
            return new TestSkeletonCodeGenerator();
        }

        private IRestClient MockProblemsApiClient()
        {
            var client = Substitute.For<IRestClient>();
            var testProblems = new List<Problem>
            {
                new Problem(),
                new Problem()
            };
            client.Execute<List<Problem>>(Arg.Any<IRestRequest>())
                .Returns(new RestResponse<List<Problem>> {Data = testProblems});
            return client;
        }

        private static IRedisClient MockRedisClient()
        {
            return Substitute.For<IRedisClient>();
        }

        private class TestSkeletonCodeGenerator : ISkeletonCodeGenerator
        {
            public (string, string) Generate(Function function)
            {
                return ("C#", "Some C# string");
            }
        }
    }
}