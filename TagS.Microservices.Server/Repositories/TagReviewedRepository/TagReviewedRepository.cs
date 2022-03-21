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

        public bool ExistedPreferredName(string preferredName)
        {
            var tagReviewedCount=_context.TagReviedweds.CountDocuments(t=>t.PreferredTagName== preferredName);
            return tagReviewedCount > 0;
        }

        public async Task<TagReviewed> GetTagReviewedAsync(string tagReviewedId)
        {
            var tag=await _context.TagReviedweds.FindAsync(t => t.Id == tagReviewedId);
            return tag.First();
        }

        public async Task<(bool result,TagReviewed? entity)> PassReviewedTagAsync(string tagReviewedId)
        {
            var reviewedTag=_context.TagReviedweds.Find(t=>t.Id==tagReviewedId).First();
            if (reviewedTag?.Statue == TagReviewedStatue.ToBeReviewed)
            {
                var update = reviewedTag.SetPassed();
                var result = await _context.TagReviedweds.UpdateOneAsync(t => t.Id == tagReviewedId, update);
                return (result.ModifiedCount == 1, reviewedTag);
            }

            return (false,null);
        }

        public async Task<(bool result, TagReviewed? entity)> RefuseReviewedTagAsync(string tagReviewedId)
        {
            var reviewedTag = _context.TagReviedweds.Find(t => t.Id == tagReviewedId).First();
            if (reviewedTag?.Statue == TagReviewedStatue.ToBeReviewed)
            {
                var update = reviewedTag.SetRefused();
                var result = await _context.TagReviedweds.UpdateOneAsync(t => t.Id == tagReviewedId, update);
                return (result.ModifiedCount == 1, reviewedTag);
            }

            return (false, null);
        }
    }
}
