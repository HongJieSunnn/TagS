using Tag= HongJieSun.TagS.Models.Tags.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.ApplicationInterface.Queries.Abstractions;
using TagS.Infrastructure.Contexts.MongoDB;
using TagS.Infrastructure.Persistences.MongoDB;
using TagS.Models.Referrers.Abstractions;
using MongoDB.Driver;

namespace TagS.ApplicationInterface.Queries.MongoDBQueries
{
    public class MongoDBTagSQueries<TReferrerId>:ITagQueries,ITagReferrerQueries<TReferrerId>
        where TReferrerId : IEquatable<TReferrerId> 
    {
        private readonly TagMongoDBContext _tagMongoDBContext;
        private readonly TagReferrerMongoDBContext<TReferrerId> _tagReferrerMongoDBContext;
        public MongoDBTagSQueries(TagMongoDBContext tagMongoDBContext,TagReferrerMongoDBContext<TReferrerId> tagReferrerMongoDBContext)
        {
            _tagMongoDBContext=tagMongoDBContext;
            _tagReferrerMongoDBContext= tagReferrerMongoDBContext;
        }

        public async Task<IEnumerable<IReferrer<TReferrerId>>> GetAllReferrersAsync()
        {
            var referrers = await _tagReferrerMongoDBContext.Referrers.FindAsync(r => true);
            return referrers.ToList();
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            var tags = await _tagMongoDBContext.Tags.FindAsync(t => true);
            return tags.ToList();
        }

        public async Task<IReferrer<TReferrerId>> GetOneReferrerByReferrerIdAsync(TReferrerId referrerId)
        {
            var referrers =await _tagReferrerMongoDBContext.Referrers.FindAsync(r => r.ReferrerId.Equals(referrerId));
            return referrers.FirstOrDefault()??throw new ArgumentException($"Referrer with ReferrerId{referrerId} is not existed.");
        }

        public async Task<IEnumerable<IReferrer<TReferrerId>>> GetManyReferrersByReferrerIdsAsync(IEnumerable<TReferrerId> referrerIds)
        {
            var referrers =await _tagReferrerMongoDBContext.Referrers.FindAsync(r => referrerIds.Contains(r.ReferrerId));
            if (!referrers.Any())
            {
                throw new ArgumentException($"The Referrers with Referrerid in referrerIds all are not existed.");
            }
            return referrers.ToList();
        }

        public async Task<Tag> GetTagByGuidAsync(Guid tagGuid)
        {
            var tag=await _tagMongoDBContext.Tags.FindAsync(t=>t.Guid.Equals(tagGuid));
            return tag.FirstOrDefault()??throw new ArgumentException($"Tag with tagGuid{tagGuid} is not existed.");
        }

        public async Task<Tag> GetTagByPreferredNameAsync(string preferredName)
        {
            var tag = await _tagMongoDBContext.Tags.FindAsync(t => t.PreferredTagName.Equals(preferredName));
            return tag.FirstOrDefault() ?? throw new ArgumentException($"Tag with PreferredName{preferredName} is not existed.");
        }

        public async Task<Tag> GetTagBySynonymAsync(string synonym)
        {
            var tag= await _tagMongoDBContext.Tags.FindAsync(t=>t.Synonyms.Contains(synonym));
            return tag.FirstOrDefault() ?? throw new ArgumentException($"Tag with synonym{synonym} is not existed.");
        }

        public Task<IEnumerable<dynamic>> GetManyReferrerCompletedInfomationsAsync(IEnumerable<IReferrer<TReferrerId>> referrer)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> GetOneReferrerCompletedInfomationsAsync(IReferrer<TReferrerId> referrer)
        {
            throw new NotImplementedException();
        }
    }
}
