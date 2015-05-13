using Careers.Api.Host.Caching;
using Careers.Api.Host.Contracts;
using Careers.Api.Host.Queries;
using Careers.Api.Host.Services;
using NSubstitute;
using Ploeh.AutoFixture;
using Should;
using Xunit;

namespace Careers.Api.Tests
{
    public class GetJobsServiceTests
    {
        [Fact]
        public void ShouldCacheSubsequantCallsToQuery()
        {
            var getJobsQuery = Substitute.For<IGetJobsQuery>();
            var getJobSpecQuery = Substitute.For<IGetJobSpecQuery>();
            var cache = new InMemoryCache();
            var getJobsService = new GetJobsService(getJobsQuery, getJobSpecQuery, cache);

            getJobsService.GetAsync();
            getJobsService.GetAsync();
            getJobsService.GetAsync();

            getJobsQuery.Received(1).Execute();
        }

        [Fact]
        public void ShouldReturnAllJobs()
        {
            var fixture = new Fixture();
            var jobs = fixture.Build<JobSummary>().CreateMany(5);
            var getJobsQuery = Substitute.For<IGetJobsQuery>();
            var getJobSpecQuery = Substitute.For<IGetJobSpecQuery>();
            getJobsQuery.Execute().Returns(jobs);
            var cache = new InMemoryCache();
            var getJobsService = new GetJobsService(getJobsQuery, getJobSpecQuery, cache);

            var results = getJobsService.GetAsync().Result;

            results.Jobs.Count.ShouldEqual(5);
        }

        [Fact]
        public void ShouldReturnJobSpec()
        {
            var fixture = new Fixture();
            var jobSpec = fixture.Create<string>();
            var getJobsQuery = Substitute.For<IGetJobsQuery>();
            var getJobSpecQuery = Substitute.For<IGetJobSpecQuery>();
            getJobSpecQuery.Execute(Arg.Any<string>()).Returns(jobSpec);
            var cache = new InMemoryCache();
            var getJobsService = new GetJobsService(getJobsQuery, getJobSpecQuery, cache);

            var results = getJobsService.GetSpecAsync("").Result;

            results.Details.ShouldEqual(jobSpec);
        }
    }
}
