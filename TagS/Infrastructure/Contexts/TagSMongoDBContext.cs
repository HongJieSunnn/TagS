using Innermost.MongoDBContext;
using Innermost.MongoDBContext.Configurations;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers.Generic;

namespace TagS.Infrastructure.Contexts
{
    public class TagSMongoDBContext<TReferrerId,TReferrer>:MongoDBContextBase
        where TReferrerId :IEquatable<TReferrerId>
        where TReferrer : IReferrer<TReferrerId>
    {
        public IMongoCollection<Tag> Tags { get; set; }
        public IMongoCollection<TReferrer> Referrers { get; set; }
        public TagSMongoDBContext(MongoDBContextConfiguration<TagSMongoDBContext<TReferrerId,TReferrer>> configuration):base(configuration)
        {

        }
    }
}
