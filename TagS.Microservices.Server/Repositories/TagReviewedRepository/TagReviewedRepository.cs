namespace TagS.Microservices.Server.Repositories.TagReviewedRepository
{
    public class TagReviewedRepository : ITagReviewedRepository
    {
        private readonly TagSMongoDBContext _context;
        public TagReviewedRepository(TagSMongoDBContext tagSMongoDBContext)
        {
            _context=tagSMongoDBContext;
        }

        public IUnitOfWork UnitOfWork => _context;

        public Task CreateReviewedTagAsync(TagReviewed tagReviewed)
        {
            return _context.TagReviedweds.InsertOneAsync(tagReviewed);
        }

        public async Task<TagReviewed> GetTagReviewedAsync(string tagReviewedId)
        {
            var tag=await _context.TagReviedweds.FindAsync(t => t.Id == tagReviewedId);
            return tag.First();
        }

        public async Task<bool> PassReviewedTagAsync(string tagReviewedId)
        {
            var reviewedTag=_context.TagReviedweds.Find(t=>t.Id==tagReviewedId).First();
            reviewedTag.SetPassed();
            var result = await _context.TagReviedweds.UpdateOneAsync(t => t.Id == tagReviewedId, reviewedTag.ToBsonDocument());
            return result.ModifiedCount == 1;
        }

        public async Task<bool> RefuseReviewedTagAsync(string tagReviewedId)
        {
            var reviewedTag = _context.TagReviedweds.Find(t => t.Id == tagReviewedId).First();
            reviewedTag.SetPassed();
            var result = await _context.TagReviedweds.UpdateOneAsync(t => t.Id == tagReviewedId, reviewedTag.ToBsonDocument());
            return result.ModifiedCount == 1;
        }
    }
}
