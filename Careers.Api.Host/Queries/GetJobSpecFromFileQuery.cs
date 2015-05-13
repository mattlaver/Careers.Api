using System.IO;

namespace Careers.Api.Host.Queries
{
    public class GetJobSpecFromFileQuery : IGetJobSpecQuery
    {
        public string Execute(string id)
        {
            const string dataPath = @"C:\easyjet\Careers.Api\Detail";

            if (!Directory.Exists(dataPath)) return string.Empty;

            var file = dataPath + "\\" + id + ".md";

            return !File.Exists(file) ? string.Empty : File.ReadAllText(file);
        }
    }
}