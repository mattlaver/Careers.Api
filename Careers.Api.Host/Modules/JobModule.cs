using System.Threading.Tasks;
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

          //  Get["jobs/{id}", runAsync: true] = async (param, token) => Response.AsJson(await _careersService.GetSpecAsync(param.));

            // Get["/jobs/", runAsync: true] = async (parameters, ct) => Response.AsJson(await _careersService.GetSpecAsync(ct));


            Get["/jobs/{id}", runAsync: true] = async (p, token) =>
            {
                string id = p.id;
                return Response.AsJson(await _careersService.GetSpecAsync(id));
            };

            //Get["/jobs/{id}"] = parameters =>
            //{
            //    var obj = Task.Run(() => _careersService.GetSpecAsync(parameters.id)).Result;


            //   // Response.AsText(obj.Details);
            //};
        }

        

    }
}