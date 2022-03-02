using Innermost.MongoDBContext.Configurations.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.ApplicationInterface.Builders;
using TagS.ApplicationInterface.Queries.Abstractions;
using TagS.ApplicationInterface.Queries.MongoDBQueries;
using TagS.Infrastructure.Contexts.MongoDB;
using TagS.Infrastructure.Persistences.MongoDB;
using TagS.Infrastructure.Repositories.Abstractions;
using TagS.Infrastructure.Repositories.MongoDBRepositories;

namespace TagS.ApplicationInterface.Configurations
{
    public class TagSConfigurationBuilder<TReferrerId>
        where TReferrerId : IEquatable<TReferrerId>
    {
        private static bool _useDatabase = false;
        private readonly TagSConfiguration<TReferrerId> _tagSConfiguration;
        public TagSConfigurationBuilder()
        {
            _tagSConfiguration = new TagSConfiguration<TReferrerId>();
        }

        public TagSConfigurationBuilder<TReferrerId> UseMongoDB(
            Action<MongoDBContextConfigurationBuilder<TagMongoDBContext>> tagMongoDBContextConfigurationAction,
            Action<MongoDBContextConfigurationBuilder<TagReferrerMongoDBContext<TReferrerId>>> tagReferrerMongoDBContextConfigurationAction
        )
        {
            if (_useDatabase)
                throw new InvalidOperationException("Database has been configured.");
            _useDatabase = true;

            _tagSConfiguration.DatabaseType = TagSConfigurationDatabaseTypes.MongoDB;
            _tagSConfiguration.TagMongoDBContextConfigurationBuilderAction= tagMongoDBContextConfigurationAction;
            _tagSConfiguration.TagReferrerMongoDBContextConfigurationBuilderAction = tagReferrerMongoDBContextConfigurationAction;

            return this;
        }

        public TagSConfigurationBuilder<TReferrerId> UseSQL(Action<DbContextOptionsBuilder> builderAction)
        {
            if (_useDatabase)
                throw new InvalidOperationException("Database has been configured.");
            _useDatabase = true;

            _tagSConfiguration.DatabaseType = TagSConfigurationDatabaseTypes.SQL;
            _tagSConfiguration.SQLDbContextOptionsBuilder = builderAction;

            return this;
        }

        public TagSConfigurationBuilder<TReferrerId> UseCQRS()
        {
            _tagSConfiguration.CQRS = true;
            return this;
        }

        public TagSConfiguration<TReferrerId> Build()
        {
            if (!_useDatabase)
                throw new InvalidOperationException("You must configure database by Methods like UseMongoDB,UseSQl etc.");
            
            return _tagSConfiguration;
        }
    }
}
