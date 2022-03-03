using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using TagS.Microservices.Client.Models;

namespace TagS.Microservices.Client.Microsoft.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTagSClient(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddReferrerDiscriminator<TReferrer>(this IServiceCollection services)
            where TReferrer : IReferrer
        {
            var existedIdProperty = typeof(TReferrer).GetProperties().Any(p => p.Name.ToUpper() == "ID");
            if (!existedIdProperty)
                throw new InvalidOperationException("Referrer should have a ID match the ID of TagableEntity");
            BsonClassMap.RegisterClassMap<TReferrer>();
            return services;
        }
    }
}
