using System.Collections.Generic;
using System.IO;
using Careers.Api.Host.Contracts;
using Newtonsoft.Json;

namespace Careers.Api.Host.Queries
{
    public class GetJobsFromFileQuery : IGetJobsQuery
    {
        public IEnumerable<JobSummary> Execute()
        {
            const string dataPath = @"C:\easyjet\Careers.Api\Data";
            var results = new List<JobSummary>();

            if (!Directory.Exists(dataPath)) return results; 
           
            foreach (var file in Directory.EnumerateFiles(dataPath, "*.json"))
            {
                try
                {
                    var contents = File.ReadAllText(file);
                    var job = JsonConvert.DeserializeObject<JobSummary>(contents);
                    results.Add(job);

                }
                catch
                {
                    
                }

            }
            return results;
        }
    }
}