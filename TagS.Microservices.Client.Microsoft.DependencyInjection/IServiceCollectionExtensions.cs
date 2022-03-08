using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using TagS.Microservices.Client.Models;
using TagS.Microservices.Client.Services;

namespace TagS.Microservices.Client.Microsoft.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTagSClient(this IServiceCollection services)
        {
            services.AddScoped<ITagIntegrationEventService, TagIntegrationEventService>();

            return services;
        }
    }
}
