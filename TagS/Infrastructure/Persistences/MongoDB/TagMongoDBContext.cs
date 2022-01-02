using Tag = HongJieSun.TagS.Models.Tags.Tag;
using Innermost.MongoDBContext;
using Innermost.MongoDBContext.Configurations;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Infrastructure.Persistences.MongoDB
{
    public class TagMongoDBContext:MongoDBContextBase
    {
        public IMongoCollection<Tag> Tags { get; set; }
        public TagMongoDBContext(MongoDBContextConfiguration<TagMongoDBContext> configuration) : base(configuration)
        {

        }
    }
}
