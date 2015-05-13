using System;
using System.Linq;
using System.Threading.Tasks;
using Careers.Api.Host.Caching;
using Careers.Api.Host.Contracts;
using Careers.Api.Host.Queries;
using Castle.Core.Logging;

namespace Careers.Api.Host.Services
{
    public class GetJobsService : IGetJobsService
    {
        private readonly IGetJobsQuery _getJobsQuery;
        private readonly IGetJobSpecQuery _getJobSpecQuery;
        private readonly ICache _cache;
        static private readonly object CacheLock = new object();
        private const int CacheMinutes = 10;
        private const string JobsCache = "jobs";
        private const string JobSpecCache = "jobspec";

        private ILogger _logger = NullLogger.Instance;

        public ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }

        public GetJobsService(IGetJobsQuery getJobsQuery, IGetJobSpecQuery getJobSpecQuery, ICache cache)
        {
            _getJobsQuery = getJobsQuery;
            _getJobSpecQuery = getJobSpecQuery;
            _cache = cache;
        }

        public async Task<GetJobsResponse> GetAsync()
        {
            return await Task.Run(() =>
                new GetJobsResponse
                {
                    Jobs =
                        _cache.RetreiveFromCache(CacheLock, JobsCache, DateTime.Now.AddMinutes(CacheMinutes),
                            () => _getJobsQuery.Execute().ToList())
                }
                );
        }

        public async Task<GetJobSpecResponse> GetSpecAsync(string id)
        {
            return await Task.Run(() =>
                new GetJobSpecResponse
                {
                    Details =
                        _cache.RetreiveFromCache(CacheLock, JobSpecCache + id, DateTime.Now.AddMinutes(CacheMinutes),
                            () => _getJobSpecQuery.Execute(id))
                }
                );
        }
    }
}