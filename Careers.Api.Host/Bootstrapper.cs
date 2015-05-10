using Careers.Api.Host.Caching;
using Careers.Api.Host.Queries;
using Careers.Api.Host.Services;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Nancy.Bootstrappers.Windsor;

namespace Careers.Api.Host
{
    public class Bootstrapper : WindsorNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IWindsorContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);         

            existingContainer.Register(Component.For<ICache>().ImplementedBy<InMemoryCache>());
            existingContainer.Register(Component.For<IGetJobsQuery>().ImplementedBy<GetJobsFromFileQuery>());
            existingContainer.Register(Component.For<IGetJobsService>().ImplementedBy<GetJobsService>());
         }      
    }
}