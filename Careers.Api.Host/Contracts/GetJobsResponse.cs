using System.Collections.Generic;

namespace Careers.Api.Host.Contracts
{
    public class GetJobsResponse
    {
        public IList<JobSummary> Jobs { get; set; } 
    }
}