using Careers.Api.Host.Caching;
using Careers.Api.Host.Queries;
using Careers.Api.Host.Services;
using Castle.Facilities.Logging;
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

            existingContainer.AddFacility<LoggingFacility>(x => x.LogUsing(LoggerImplementation.Log4net).WithConfig("log4net.config"));

            existingContainer.Register(Component.For<ICache>().ImplementedBy<InMemoryCache>());
            existingContainer.Register(Component.For<IGetJobsQuery>().ImplementedBy<GetJobsFromFileQuery>());
            existingContainer.Register(Component.For<IGetJobSpecQuery>().ImplementedBy<GetJobSpecFromFileQuery>());
            existingContainer.Register(Component.For<IGetJobsService>().ImplementedBy<GetJobsService>());
         }      
    }
}