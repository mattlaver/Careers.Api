using System.Configuration;
using System.IO;
using Castle.Core.Logging;

namespace Careers.Api.Host.Queries
{
    public class GetJobSpecFromFileQuery : IGetJobSpecQuery
    {
        private readonly string _dataPath = ConfigurationManager.AppSettings["JobSpecDirectory"];
        private ILogger _logger = NullLogger.Instance;

        public ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }
        
        public string Execute(string id)
        {
            if (!Directory.Exists(_dataPath))
            {
                _logger.Error(string.Format("Job Spec directory does not exists: {0}", _dataPath));
                return string.Empty;
            }

            var file = _dataPath + "\\" + id + ".md";

            if (File.Exists(file)) return File.ReadAllText(file);
            
            _logger.Error(string.Format("Job Spec file does not exists: {0}", _dataPath));
            return string.Empty;
        }
    }
}