using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Careers.Api.Host.Contracts;
using Castle.Core.Logging;
using Newtonsoft.Json;

namespace Careers.Api.Host.Queries
{
    public class GetJobsFromFileQuery : IGetJobsQuery
    {
        private readonly string _dataPath = ConfigurationManager.AppSettings["JobSummaryDirectory"]; 
        private ILogger _logger = NullLogger.Instance;

        public ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }

        public IEnumerable<JobSummary> Execute()
        {            
            var results = new List<JobSummary>();

            if (!Directory.Exists(_dataPath))
            {
                _logger.Error(string.Format("Job Summary directory does not exists: {0}", _dataPath));
                return results;
            } 
           
            foreach (var file in Directory.EnumerateFiles(_dataPath, "*.json"))
            {
                try
                {
                    var contents = File.ReadAllText(file);
                    var job = JsonConvert.DeserializeObject<JobSummary>(contents);
                    results.Add(job);

                }
                catch (Exception e)
                {
                    _logger.Error(string.Format("Exception parsing file: {0}", e.Message));
                }

            }
            return results;
        }
    }
}