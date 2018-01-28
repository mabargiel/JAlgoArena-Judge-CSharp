using System;
using System.Collections.Generic;
using Judge.Infrastructure.Generators;
using Judge.Infrastructure.ProblemsSchema;
using RestSharp;
using ServiceStack.Redis;

namespace Judge.Infrastructure.Data.Repositories
{
    public class CachedProblemsRepository : IProblemsRepository
    {
        private readonly IRedisClient _redisClient;
        private readonly IRestClient _restClient;
        private readonly ISkeletonCodeGenerator _skeletonCodeGenerator;

        public CachedProblemsRepository(IRedisClient redisClient, IRestClient restClient, ISkeletonCodeGenerator skeletonCodeGenerator)
        {
            _redisClient = redisClient;
            _restClient = restClient;
            _skeletonCodeGenerator = skeletonCodeGenerator;
        }

        public List<Problem> GetAll()
        {
            var request = new RestRequest("problems", Method.GET);
            var response = _restClient.Execute<List<Problem>>(request);

            var result = response?.Data ?? new List<Problem>();
            result.ForEach(problem => _skeletonCodeGenerator.GenerateFor(problem));

            _redisClient.StoreAll<Problem>(result);

            return result;
        }

        public Problem FindById()
        {
            throw new NotImplementedException();
        }
    }
}