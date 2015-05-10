using System;
using System.Linq;
using System.Threading.Tasks;
using Careers.Api.Host.Caching;
using Careers.Api.Host.Contracts;
using Careers.Api.Host.Queries;

namespace Careers.Api.Host.Services
{
    public class GetJobsService : IGetJobsService
    {
        private readonly IGetJobsQuery _getJobsQuery;
        private readonly ICache _cache;
        static private readonly object CacheLock = new object();
        private const int CacheMinutes = 10;

        public GetJobsService(IGetJobsQuery getJobsQuery, ICache cache)
        {
            _getJobsQuery = getJobsQuery;
            _cache = cache;
        }

        public async Task<GetJobsResponse> GetAsync()
        {
            return await Task.Run(() =>
                new GetJobsResponse
                {
                    Jobs = _cache.RetreiveFromCache(CacheLock, "jobs", DateTime.Now.AddMinutes(CacheMinutes), () => _getJobsQuery.Execute().ToList())
                }
            );
        }
    }
}