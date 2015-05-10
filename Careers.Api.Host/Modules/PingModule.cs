using Nancy;

namespace Careers.Api.Host.Modules
{
    public class PingModule : NancyModule
    {
        public PingModule()
        {
            Get["/ping"] = _ => "pong";
        }
    }
}