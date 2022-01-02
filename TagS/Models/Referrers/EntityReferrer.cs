using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers.Generic;

namespace TagS.Models.Referrers
{
    public class EntityReferrer<TReferrerId> : IReferrer<TReferrerId>
        where TReferrerId : IEquatable<TReferrerId>
    {
        public ObjectId ObjectId { get; init; }
        public TReferrerId ReferrerId { get; init; }
        public string From { get; set; }
        public IEnumerable<string> Tags { get; set; }
        protected EntityReferrer()
        {
            Tags=new List<string>();
        }

        public EntityReferrer(ObjectId id,TReferrerId referrerId,string from):this()
        {
            ObjectId = id;
            ReferrerId = referrerId;
            From = from;
        }

        public EntityReferrer(ObjectId id, TReferrerId referrerId, string from, IEnumerable<string> tags)
        {
            ObjectId = id;
            ReferrerId = referrerId;
            From = from;
            Tags=tags;
        }
    }
}
