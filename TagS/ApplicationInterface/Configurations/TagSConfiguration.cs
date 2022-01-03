using Innermost.MongoDBContext.Configurations.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Infrastructure.Contexts.MongoDB;
using TagS.Infrastructure.Persistences.MongoDB;

namespace TagS.ApplicationInterface.Configurations
{
    public class TagSConfiguration<TReferrerId>
        where TReferrerId : IEquatable<TReferrerId>
    {
        public bool CQRS { get; set; } = false;
        public TagSConfigurationDatabaseTypes DatabaseType { get; set; }
        /// <summary>
        /// whether has configured the database context.
        /// </summary>
        public Action<MongoDBContextConfigurationBuilder<TagMongoDBContext>>? TagMongoDBContextConfigurationBuilderAction { get; set; }
        public Action<MongoDBContextConfigurationBuilder<TagReferrerMongoDBContext<TReferrerId>>>? TagReferrerMongoDBContextConfigurationBuilderAction { get; set; }
        public Action<DbContextOptionsBuilder>? SQLDbContextOptionsBuilder { get; set; }//TODO TagSSQLContexts

        public TagSConfiguration()
        {
            DatabaseType = TagSConfigurationDatabaseTypes.None;
        }
    }

    public enum TagSConfigurationDatabaseTypes
    {
        None=0,
        MongoDB=1,
        SQL=2,
    }
}
