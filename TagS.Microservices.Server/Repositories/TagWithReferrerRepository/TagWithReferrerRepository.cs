namespace TagS.Microservices.Server.Repositories.TagWithReferrerRepository
{
    public class TagWithReferrerRepository : ITagWithReferrerRepository
    {
        private readonly TagSMongoDBContext _context;
        public TagWithReferrerRepository(TagSMongoDBContext tagSMongoDBContext)
        {
            _context =  tagSMongoDBContext;
        }

        public Task AddAsync(TagWithReferrer tagWithReferrer)
        {
            return _context.TagWithReferrers.InsertOneAsync(tagWithReferrer);
        }

        public Task<DeleteResult> DeleteAsync(string tagId)
        {
            return _context.TagWithReferrers.DeleteOneAsync(t=>t.Id==tagId);
        }

        public Task AddReferrerToTagAsync(string tagId, IReferrer referrer)
        {
            var updateModel=Builders<TagWithReferrer>.Update.AddToSet(tr=>tr.Referrers,referrer);
            return _context.TagWithReferrers.UpdateOneAsync(t => t.Id == tagId, updateModel);
        }

        public Task RemoveReferrerToTagAsync(string tagId, IReferrer referrer)
        {
            var updateModel = Builders<TagWithReferrer>.Update.Pull(tr => tr.Referrers, referrer);
            return _context.TagWithReferrers.UpdateOneAsync(t => t.Id == tagId, updateModel);
        }

        public Task ChangeTagDetailAsync(string tagId, string detail)
        {
            var updateModel = Builders<TagWithReferrer>.Update.Set(t => t.TagDetail, detail);
            return _context.TagWithReferrers.UpdateOneAsync(t => t.Id == tagId, updateModel);
        }

        public Task AddSynonymAsync(string tagId, string synonym)
        {
            var updateModel = Builders<TagWithReferrer>.Update.AddToSet(tr => tr.Synonyms, synonym);
            return _context.TagWithReferrers.UpdateOneAsync(t => t.Id == tagId, updateModel);
        }

        public Task RemoveSynonymAsync(string tagId, string synonym)
        {
            var updateModel = Builders<TagWithReferrer>.Update.Pull(tr => tr.Synonyms, synonym);
            return _context.TagWithReferrers.UpdateOneAsync(t => t.Id == tagId, updateModel);
        }
    }
}
