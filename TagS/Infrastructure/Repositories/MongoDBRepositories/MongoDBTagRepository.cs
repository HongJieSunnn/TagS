using Tag= HongJieSun.TagS.Models.Tags.Tag;
using Innermost.MongoDBContext;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Infrastructure.Persistences.MongoDB;
using TagS.Infrastructure.Repositories.Abstractions;
using TagS.Models.Referrers.Abstractions;
using MongoDB.Driver;

namespace TagS.Infrastructure.Repositories.MongoDBRepositories
{
    internal class MongoDBTagRepository : ITagRepository<TagMongoDBContext>
    {
        private readonly TagMongoDBContext _tagMongoDBContext;
        public MongoDBTagRepository(TagMongoDBContext tagMongoDBContext)
        {
            _tagMongoDBContext = tagMongoDBContext;
        }

        public bool Existed(Tag tag)
        {
            var tagToFind = _tagMongoDBContext.Tags.FindSync(t=>t.Guid == tag.Guid||t.PreferredTagName==tag.PreferredTagName);
            return tagToFind != null;
        }

        public bool Existed(Guid tagGuid)
        {
            var tagToFind = _tagMongoDBContext.Tags.FindSync(t => t.Guid == tagGuid);
            return tagToFind != null;
        }

        public bool Existed(IEnumerable<Guid> tagGuids)
        {
            var tagCount = _tagMongoDBContext.Tags.FindSync(t => tagGuids.Contains(t.Guid)).ToList().Count;
            return tagCount==tagGuids.Count();
        }

        public bool Existed(string preferredTagName)
        {
            var tagToFind = _tagMongoDBContext.Tags.FindSync(t => t.PreferredTagName == preferredTagName);
            return tagToFind != null;
        }

        public async Task<Tag> GetTagByGuidAsync(Guid tagGuid)
        {
            var tags = await _tagMongoDBContext.Tags.FindAsync(t => t.Guid == tagGuid);
            return tags.FirstOrDefault();
        }

        public async Task<Tag> GetTagByPreferredNameAsync(string preferredName)
        {
            var tags = await _tagMongoDBContext.Tags.FindAsync(t => t.PreferredTagName == preferredName);
            return tags.FirstOrDefault();
        }

        public async Task AddAsync(Tag tag)
        {
            await _tagMongoDBContext.Tags.InsertOneAsync(tag);
        }

        public async Task UpdateAsync(Tag tag)
        {
            await _tagMongoDBContext.Tags.UpdateOneAsync(t=>t.Guid==tag.Guid,tag.ToBsonDocument());
        }


        public async Task AddReferrerAsync(Guid tagGuid, Guid referrerGuid)
        {
            var tags = await _tagMongoDBContext.Tags.FindAsync(t => t.Guid == tagGuid);
            var tag = tags.FirstOrDefault();

            tag.Referrers.Add(referrerGuid);//HashSet itself is Deduplicated.

            await _tagMongoDBContext.Tags.UpdateOneAsync(t=>t.Guid == tagGuid,tag.ToBsonDocument());
        }

        public async Task RemoveReferrerAsync(Guid tagGuid, Guid referrerGuid)
        {
            var tags = await _tagMongoDBContext.Tags.FindAsync(t => t.Guid == tagGuid);
            var tag = tags.FirstOrDefault();

            if(tag.Referrers.Contains(referrerGuid))
                tag.Referrers.Remove(referrerGuid);//HashSet itself is Deduplicated.

            await _tagMongoDBContext.Tags.UpdateOneAsync(t => t.Guid == tagGuid, tag.ToBsonDocument());
        }

        public async Task DeleteAsync(Tag tag)
        {
            await _tagMongoDBContext.Tags.FindOneAndDeleteAsync(t => t.Guid == tag.Guid || t.PreferredTagName == tag.PreferredTagName);
        }

        public async Task DeleteAsync(Guid tagGuid)
        {
            await _tagMongoDBContext.Tags.FindOneAndDeleteAsync(t => t.Guid == tagGuid);
        }
    }
}
