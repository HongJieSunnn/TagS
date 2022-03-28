using EventBusCommon.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using TagS.Microservices.Client.Services;

namespace TagS.Microservices.Client.Microsoft.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTagSClientMongoDBIntegrationEventService(this IServiceCollection services)
        {
            services.AddScoped<ITagIntegrationEventService, TagIntegrationEventService>();
            services.AddScoped<IIntegrationEventService, TagIntegrationEventService>();

            return services;
        }
    }
}
