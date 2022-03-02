using Innermost.MongoDBContext;
using Innermost.MongoDBContext.Configurations;
using TagS.Microservices.Server.Models;
using Tag = TagS.Microservices.Server.Models.Tag;

namespace TagS.Microservices.Server
{
    public class TagSMongoDBContext:MongoDBContextBase
    {
        public IMongoCollection<TagWithReferrer> TagWithReferrers { get; set; }
        public IMongoCollection<Tag> Tags { get; set; }
        public TagSMongoDBContext(MongoDBContextConfiguration<TagSMongoDBContext> mongoDB):base(mongoDB)
        {

        }
    }
}
