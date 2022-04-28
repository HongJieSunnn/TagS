using EventBusCommon.Abstractions;
using IntegrationEventServiceMongoDB.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace TagS.Microservices.Client.Microsoft.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTagSClient(this IServiceCollection services)
        {
            

            return services;
        }
    }
}
