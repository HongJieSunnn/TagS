namespace TagS.Microservices.Server.Repositories.TagWithReferrerRepository
{
    public class TagWithReferrerRepository : ITagWithReferrerRepository
    {
        private readonly TagSMongoDBContext _context;
        private readonly IClientSessionHandle _session;
        private readonly ILogger<TagWithReferrerRepository> _logger;
        public TagWithReferrerRepository(TagSMongoDBContext tagSMongoDBContext, IClientSessionHandle session, ILogger<TagWithReferrerRepository> logger)
        {
            _context = tagSMongoDBContext;
            _session = session;
            _logger = logger;
        }

        public Task AddAsync(TagWithReferrer tagWithReferrer)
        {
            return _context.TagWithReferrers.InsertOneAsync(_session, tagWithReferrer);
        }

        public Task<UpdateResult> DeleteAsync(string tagId)
        {
            var update = Builders<TagWithReferrer>.Update.Set(t => t.DeleteTime, DateTime.Now);
            return _context.TagWithReferrers.UpdateOneAsync(_session, t => t.Id == tagId, update);
        }

        public Task AddReferrerToTagAsync(string tagId, IReferrer referrer)
        {
            var updateModel = Builders<TagWithReferrer>.Update.AddToSet(tr => tr.Referrers, referrer).Set(tr => tr.UpdateTime, DateTime.Now);
            return _context.TagWithReferrers.UpdateOneAsync(_session, t => t.Id == tagId, updateModel);
        }

        public Task RemoveReferrerToTagAsync(string tagId, IReferrer referrer)
        {
            var tagWithReferrer = _context.TagWithReferrers.Find(t => t.Id == tagId).First();
            //I have override the Equals methods in ReferrerBase and it's useful in memory.
            //But if we use Update.Pull(tr=>tr.Referrers,referrer) it can work.So we need use the PullFirter.
            //I guess the reason is mongodb uses Id to determine which item to pull.
            var pullFilter = Builders<IReferrer>.Filter.Eq("ReferrerId", referrer.ReferrerId) & Builders<IReferrer>.Filter.Eq("ReferrerName", referrer.ReferrerName);
            var updateModel = Builders<TagWithReferrer>.Update
                .PullFilter(tr => tr.Referrers, pullFilter)
                .Set(tr => tr.UpdateTime, DateTime.Now);
            return _context.TagWithReferrers.UpdateOneAsync(_session, t => t.Id == tagId, updateModel);
        }

        public Task<UpdateResult> UpdateAsync(string tagId, UpdateDefinition<TagWithReferrer> updateDefinition, params FilterDefinition<TagWithReferrer>[] filterDefinitions)
        {
            var filter = CombineFilterDefinitions(Builders<TagWithReferrer>.Filter.Eq(t => t.Id, tagId), filterDefinitions);

            return _context.TagWithReferrers.UpdateOneAsync(_session, filter, updateDefinition);
        }

        private FilterDefinition<TagWithReferrer> CombineFilterDefinitions(FilterDefinition<TagWithReferrer> firstFilterDefinition, IEnumerable<FilterDefinition<TagWithReferrer>> filterDefinitions)
        {
            var filter = firstFilterDefinition;
            foreach (var filterDefinition in filterDefinitions)
            {
                filter &= filterDefinition;
            }
            return filter;
        }
    }
}
