using HongJieSun.TagS.Models.Tags;
using Innermost.MongoDBContext;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Infrastructure.Contexts.MongoDB;
using TagS.Infrastructure.Repositories.Abstractions;
using TagS.Models.Referrers.Abstractions;
using MongoDB.Driver;

namespace TagS.Infrastructure.Repositories.MongoDBRepositories
{
    public class MongoDBTagReferrerRepository<TReferrerId> : ITagReferrerRepository<TReferrerId>
        where TReferrerId : IEquatable<TReferrerId>
    {
        private readonly TagReferrerMongoDBContext<TReferrerId> _tagReferrerMongoDBContext;
        public MongoDBTagReferrerRepository(TagReferrerMongoDBContext<TReferrerId> tagReferrerMongoDBContext)
        {
            _tagReferrerMongoDBContext=tagReferrerMongoDBContext;
        }

        public bool Existed(Guid referrerGuid)
        {
            var referrer=_tagReferrerMongoDBContext.Referrers.FindSync(r=>r.Guid== referrerGuid).FirstOrDefault();
            return referrer!=null;
        }

        public bool Existed(IEnumerable<Guid> referrerGuids)
        {
            var referrerCount = _tagReferrerMongoDBContext.Referrers.FindSync(r => referrerGuids.Contains(r.Guid)).ToList().Count;
            return referrerCount == referrerGuids.Count();
        }

        public bool Existed(IReferrer<TReferrerId> referrer)
        {
            if (referrer.Guid == Guid.Empty)
                return false;
            return Existed(referrer.Guid);
        }

        public IReferrer<TReferrerId> GetReferrerByGuid(Guid referrerGuid)
        {
            var referrer = _tagReferrerMongoDBContext.Referrers.FindSync(r => r.Guid == referrerGuid).FirstOrDefault();
            return referrer;
        }

        public IReferrer<TReferrerId> GetReferrerByReferrerId(TReferrerId referrerId)
        {
            var referrer = _tagReferrerMongoDBContext.Referrers.FindSync(r => r.ReferrerId.Equals(referrerId)).FirstOrDefault();
            return referrer;
        }

        public async Task<IReferrer<TReferrerId>> GetReferrerByReferrerIdAsync(TReferrerId referrerId)
        {
            var referrer = await _tagReferrerMongoDBContext.Referrers.FindAsync(r => r.ReferrerId.Equals(referrerId));
            return referrer.FirstOrDefault();
        }

        public async Task<IReferrer<TReferrerId>> GetReferrerByGuidAsync(Guid referrerGuid)
        {
            var referrer = await _tagReferrerMongoDBContext.Referrers.FindAsync(r => r.Guid.Equals(referrerGuid));
            return referrer.FirstOrDefault();
        }

        public async Task AddAsync(IReferrer<TReferrerId> referrer)
        {
            await _tagReferrerMongoDBContext.Referrers.InsertOneAsync(referrer);
        }

        public async Task UpdateAsync(IReferrer<TReferrerId> referrer)
        {
            await _tagReferrerMongoDBContext.Referrers.UpdateOneAsync(r => r.Guid == referrer.Guid, referrer.ToBsonDocument());
        }

        public async Task DeleteAsync(IReferrer<TReferrerId> referrer)
        {
            await _tagReferrerMongoDBContext.Referrers.FindOneAndDeleteAsync(r => r.Guid == referrer.Guid);
        }
    }
}
