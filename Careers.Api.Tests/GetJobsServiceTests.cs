using Careers.Api.Host.Caching;
using Careers.Api.Host.Queries;
using Careers.Api.Host.Services;
using NSubstitute;
using Xunit;

namespace Careers.Api.Tests
{
    public class GetJobsServiceTests
    {
        [Fact]
        public void ShouldCacheSubsequantCallsToQuery()
        {
            var getJobsQuery = Substitute.For<IGetJobsQuery>();
            var cache = new InMemoryCache();
            var getJobsService = new GetJobsService(getJobsQuery, cache);

            getJobsService.GetAsync();
            getJobsService.GetAsync();
            getJobsService.GetAsync();

            getJobsQuery.Received(1).Execute();
        }
    }
}
