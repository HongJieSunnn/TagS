using Innermost.MongoDBContext.Configurations;
using Innermost.MongoDBContext.Configurations.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.ApplicationInterface.Configurations;
using TagS.ApplicationInterface.Queries.Abstractions;
using TagS.ApplicationInterface.Queries.MongoDBQueries;
using TagS.ApplicationInterface.TagManagers;
using TagS.ApplicationInterface.TagReferrerManagers;
using TagS.Infrastructure.Contexts.MongoDB;
using TagS.Infrastructure.Persistences.MongoDB;
using TagS.Infrastructure.Repositories.Abstractions;
using TagS.Infrastructure.Repositories.MongoDBRepositories;

namespace TagS.ApplicationInterface.Builders
{
    /// <summary>
    /// TagSBuilder is used for the desktop or console app.
    /// ASP web app need to use extend method AddTagS .
    /// </summary>
    /// <typeparam name="TReferrerId"></typeparam>
    public class TagSBuilder<TReferrerId>
        where TReferrerId : IEquatable<TReferrerId>
    {
        private const string NotInitialString = "You must initial TagSConfiguration by UseConfiguration method.";
        private static TagSConfiguration<TReferrerId>? _tagSConfiguration;
        private static ITagManager? _tagManager;
        private static ITagReferrerManager<TReferrerId>? _tagReferrerManager;
        private static ITagSQueries? _tagSQueries;
        protected TagSBuilder()
        {
            
        }

        //TODO BuildeReferrer

        public static void UseConfiguration(Func<TagSConfiguration<TReferrerId>> func)
        {
            _tagSConfiguration = func();
            switch (_tagSConfiguration.DatabaseType)
            {
                case TagSConfigurationDatabaseTypes.None:
                    throw new ArgumentException("TagS configuration must identify a database type.");
                case TagSConfigurationDatabaseTypes.MongoDB:
                    BuildWithMongoDB();
                    break;
                case TagSConfigurationDatabaseTypes.SQL:
                    throw new NotImplementedException();
                    break;
                default:
                    break;
            }
        }

        public static ITagManager BuildTagManager()
        {
            return _tagManager ?? throw new InvalidOperationException(NotInitialString);
        }

        public static ITagReferrerManager<TReferrerId> BuildTagReferrerManager()
        {
            return _tagReferrerManager ?? throw new InvalidOperationException(NotInitialString);
        }

        public static ITagSQueries BuildTagSQueries()
        {
            return _tagSQueries??throw new InvalidOperationException(NotInitialString);
        }

        private static void BuildWithMongoDB()
        {
            var tagMongoDBContextConfigurationBuilder = new MongoDBContextConfigurationBuilder<TagMongoDBContext>();
            var tagReferrerMongoDBContextConfigurationBuilder = new MongoDBContextConfigurationBuilder<TagReferrerMongoDBContext<TReferrerId>>();

            _tagSConfiguration!.TagMongoDBContextConfigurationBuilderAction!(tagMongoDBContextConfigurationBuilder);
            _tagSConfiguration!.TagReferrerMongoDBContextConfigurationBuilderAction!(tagReferrerMongoDBContextConfigurationBuilder);


            var tagMongoDBContext = new TagMongoDBContext(tagMongoDBContextConfigurationBuilder.Build());
            var tagReferrerMongoDBContext = new TagReferrerMongoDBContext<TReferrerId>(tagReferrerMongoDBContextConfigurationBuilder.Build());

            var tagRepository=new MongoDBTagRepository(tagMongoDBContext);
            var tagReferrerRepository = new MongoDBTagReferrerRepository<TReferrerId>(tagReferrerMongoDBContext);

            _tagManager = new TagManager(tagRepository);
            _tagReferrerManager = new TagReferrerManager<TReferrerId>(tagRepository,tagReferrerRepository);
            _tagSQueries = new MongoDBTagSQueries<TReferrerId>(tagMongoDBContext, tagReferrerMongoDBContext);
        }
    }
}
