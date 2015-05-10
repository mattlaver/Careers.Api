using System.Collections.Generic;
using Careers.Api.Host.Contracts;

namespace Careers.Api.Host.Queries
{
    public interface IGetJobsQuery
    {
        IEnumerable<JobSummary> Execute();
    }
}