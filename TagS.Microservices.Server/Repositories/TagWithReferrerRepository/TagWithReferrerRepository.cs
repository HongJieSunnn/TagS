﻿namespace TagS.Microservices.Server.Repositories.TagWithReferrerRepository
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

        public Task<UpdateResult> DeleteAsync(string tagId)
        {
            var update = Builders<TagWithReferrer>.Update.Set(t => t.DeleteTime, DateTime.Now);
            return _context.TagWithReferrers.UpdateOneAsync(t => t.Id == tagId, update);
        }

        public Task AddReferrerToTagAsync(string tagId, IReferrer referrer)
        {
            var updateModel=Builders<TagWithReferrer>.Update.AddToSet(tr=>tr.Referrers,referrer).Set(tr=>tr.UpdateTime, DateTime.Now);
            return _context.TagWithReferrers.UpdateOneAsync(t => t.Id == tagId, updateModel);
        }

        public Task RemoveReferrerToTagAsync(string tagId, IReferrer referrer)
        {
            var updateModel = Builders<TagWithReferrer>.Update.Pull(tr => tr.Referrers, referrer).Set(tr => tr.UpdateTime, DateTime.Now);
            return _context.TagWithReferrers.UpdateOneAsync(t => t.Id == tagId, updateModel);
        }

        public Task ChangeTagDetailAsync(string tagId, string detail)
        {
            var updateModel = Builders<TagWithReferrer>.Update.Set(t => t.TagDetail, detail).Set(tr => tr.UpdateTime, DateTime.Now);
            return _context.TagWithReferrers.UpdateOneAsync(t => t.Id == tagId, updateModel);
        }

        public Task AddSynonymAsync(string tagId, string synonym)
        {
            var updateModel = Builders<TagWithReferrer>.Update.AddToSet(tr => tr.Synonyms, synonym).Set(tr => tr.UpdateTime, DateTime.Now);
            return _context.TagWithReferrers.UpdateOneAsync(t => t.Id == tagId, updateModel);
        }

        public Task RemoveSynonymAsync(string tagId, string synonym)
        {
            var updateModel = Builders<TagWithReferrer>.Update.Pull(tr => tr.Synonyms, synonym).Set(tr => tr.UpdateTime, DateTime.Now);
            return _context.TagWithReferrers.UpdateOneAsync(t => t.Id == tagId, updateModel);
        }
    }
}
