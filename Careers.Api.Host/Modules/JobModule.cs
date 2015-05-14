using Careers.Api.Host.Services;
using Nancy;

namespace Careers.Api.Host.Modules
{
    public class JobModule : NancyModule
    {
        private readonly IGetJobsService _careersService;

        public JobModule(IGetJobsService careersService)
        {
            _careersService = careersService;

            Get["/jobs", runAsync: true] = async (_, token) => Response.AsJson(await _careersService.GetAsync());

            Get["/jobs/{id}", runAsync: true] = async (p, token) =>
            {
                string id = p.id;
                return Response.AsJson(await _careersService.GetSpecAsync(id));
            };
        }
    }
}