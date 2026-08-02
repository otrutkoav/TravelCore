using System.Web.Http;
using TourCore.Search.Application.Abstractions;
using TourCore.Search.Application.Handlers;
using TourCore.Search.Application.Validators;
using TourCore.Search.Contracts;
using TourCore.Search.Infrastructure.SqlServer.Persistence;
using TourCore.Search.Infrastructure.SqlServer.Queries;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace TourCore.Search.Api.Legacy
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<SearchReadDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<ITourSearchDataSource, Ef6TourSearchDataSource>(new HierarchicalLifetimeManager());
            container.RegisterType<TourSearchRequestValidator>();
            container.RegisterType<
                IQueryHandler<TourSearchRequest, TourSearchResponse>,
                TourSearchHandler>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
