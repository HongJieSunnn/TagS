using Innermost.MongoDBContext;
using Innermost.MongoDBContext.Configurations;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers.Abstractions;

namespace TagS.Infrastructure.Contexts.MongoDB
{
    public class TagReferrerMongoDBContext<TReferrerId>:MongoDBContextBase
        where TReferrerId :IEquatable<TReferrerId>
    {
        public IMongoCollection<IReferrer<TReferrerId>> Referrers { get; set; }
        public TagReferrerMongoDBContext(MongoDBContextConfiguration<TagReferrerMongoDBContext<TReferrerId>> configuration):base(configuration)
        {

        }
    }
}
