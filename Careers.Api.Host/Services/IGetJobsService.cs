using System.Threading.Tasks;
using Careers.Api.Host.Contracts;

namespace Careers.Api.Host.Services
{
    public interface IGetJobsService
    {
        Task<GetJobsResponse> GetAsync();
        Task<GetJobSpecResponse> GetSpecAsync(string id);
    }
}
