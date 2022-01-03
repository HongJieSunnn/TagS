using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers.Abstractions;

namespace TagS.Models.Referrers.Enumeration.MongoDB
{
    public class MongoDBEntityReferrer<TReferrerId> : IReferrer<TReferrerId>
        where TReferrerId : IEquatable<TReferrerId>
    {
        [BsonId]
        public Guid Guid { get; set; }
        public TReferrerId ReferrerId { get; init; }
        public string TagableTypeJson { get; set; }
        public string FromType { get; set; }
        public string FromJson { get; set; }
        public List<TagIdentityModel> Tags { get; set; }

        public MongoDBEntityReferrer(Guid guid,TReferrerId referrerId,string tagableTypeJson,string fromType,string fromJson,List<TagIdentityModel> tagIdentityModels)
        {
            Guid = guid;
            ReferrerId = referrerId??throw new ArgumentNullException(nameof(referrerId));
            TagableTypeJson = tagableTypeJson;
            FromType = fromType;
            FromJson = fromJson;
            Tags = tagIdentityModels;
        }
    }
}
