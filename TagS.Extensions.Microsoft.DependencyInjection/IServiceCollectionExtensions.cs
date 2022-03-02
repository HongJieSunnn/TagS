using Innermost.MongoDBContext.Configurations.Builder;
using Innermost.MongoDBContext.Extensions.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using TagS.ApplicationInterface.Configurations;
using TagS.ApplicationInterface.TagManagers;
using TagS.ApplicationInterface.TagReferrerManagers;
using TagS.Infrastructure.Contexts.MongoDB;
using TagS.Infrastructure.Persistences.MongoDB;
using TagS.Infrastructure.Repositories.Abstractions;
using TagS.Infrastructure.Repositories;
using TagS.Infrastructure.Repositories.MongoDBRepositories;
using TagS.ApplicationInterface.Queries.Abstractions;
using TagS.ApplicationInterface.Queries.MongoDBQueries;

namespace TagS.Extensions.Microsoft.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTagS<TReferrerId>(this IServiceCollection services, Action<TagSConfigurationBuilder<TReferrerId>> configutionBuilderAction) where TReferrerId:IEquatable<TReferrerId>
        {
            var configurationBuilder=new TagSConfigurationBuilder<TReferrerId>();
            configutionBuilderAction(configurationBuilder);
            var configuration=configurationBuilder.Build();

            switch (configuration.DatabaseType)
            {
                case TagSConfigurationDatabaseTypes.None:
                    throw new ArgumentException("TagS configuration must identify a database type.");
                case TagSConfigurationDatabaseTypes.MongoDB:
                    services.AddTagSUseMongoDB<TReferrerId>(
                        configuration.TagMongoDBContextConfigurationBuilderAction ?? throw new NullReferenceException(nameof(configuration.TagMongoDBContextConfigurationBuilderAction)), 
                        configuration.TagReferrerMongoDBContextConfigurationBuilderAction?? throw new NullReferenceException(nameof(configuration.TagReferrerMongoDBContextConfigurationBuilderAction))
                    );
                    break;
                case TagSConfigurationDatabaseTypes.SQL:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new InvalidOperationException();
            }

            services.AddScoped<ITagManager, TagManager>();
            services.AddScoped<ITagReferrerManager<TReferrerId>, TagReferrerManager<TReferrerId>>();

            return services;
        }

        private static IServiceCollection AddTagSUseMongoDB<TReferrerId>(
            this IServiceCollection services,
            Action<MongoDBContextConfigurationBuilder<TagMongoDBContext>> tagMongoDBContextConfigurationBuilderAction,
            Action<MongoDBContextConfigurationBuilder<TagReferrerMongoDBContext<TReferrerId>>> tagReferrerMongoDBContextConfigurationBuilderAction
        )
            where TReferrerId : IEquatable<TReferrerId>
        {
            services.AddScoped<ITagRepository, MongoDBTagRepository>();
            services.AddScoped<ITagReferrerRepository<TReferrerId>, MongoDBTagReferrerRepository<TReferrerId>>();

            services.AddScoped<ITagSQueries, MongoDBTagSQueries<TReferrerId>>();

            services.AddMongoDBContext<TagMongoDBContext>(tagMongoDBContextConfigurationBuilderAction);
            services.AddMongoDBContext<TagReferrerMongoDBContext<TReferrerId>> (tagReferrerMongoDBContextConfigurationBuilderAction);

            return services;
        }
    }
}