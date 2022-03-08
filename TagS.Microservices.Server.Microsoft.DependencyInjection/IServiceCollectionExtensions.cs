using Innermost.MongoDBContext.Configurations.Builder;
using Innermost.MongoDBContext.Extensions.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using TagS.Microservices.Client.Models;
using TagS.Microservices.Server.Queries.TagQueries;
using TagS.Microservices.Server.Queries.TagReviewedQueries;
using TagS.Microservices.Server.Queries.TagWithReferrerQueries;
using TagS.Microservices.Server.Repositories.TagRepository;
using TagS.Microservices.Server.Repositories.TagReviewedRepository;
using TagS.Microservices.Server.Repositories.TagWithReferrerRepository;

namespace TagS.Microservices.Server.Microsoft.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTagSServer(this IServiceCollection services, Action<MongoDBContextConfigurationBuilder<TagSMongoDBContext>> mongoDBConfigurationAction)
        {
            services
                .AddMongoDBContext<TagSMongoDBContext>(mongoDBConfigurationAction)
                .AddTagSServerQueriesAndRepositories();

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

        private static IServiceCollection AddTagSServerQueriesAndRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITagQueries, TagQueries>();
            services.AddScoped<ITagWithReferrerQueries, TagWithReferrerQueries>();
            services.AddScoped<ITagReviewedQueries, TagReviewedQueries>();

            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagWithReferrerRepository, TagWithReferrerRepository>();
            services.AddScoped<ITagReviewedRepository, TagReviewedRepository>();

            return services;
        }
    }
}
