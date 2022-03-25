namespace TagS.Microservices.Server.Repositories.TagReviewedRepository
{
    public class TagReviewedRepository : ITagReviewedRepository
    {
        private readonly TagSMongoDBContext _context;
        private readonly IClientSessionHandle _session;
        public TagReviewedRepository(TagSMongoDBContext tagSMongoDBContext, IClientSessionHandle session)
        {
            _context = tagSMongoDBContext;
            _session = session;
        }

        public IUnitOfWork UnitOfWork => _context;

        public Task CreateReviewedTagAsync(TagReviewed tagReviewed)
        {
            return _context.TagRevieweds.InsertOneAsync(_session,tagReviewed);
        }

        public bool ExistedPreferredName(string preferredName)
        {
            var tagReviewedCount=_context.TagRevieweds.CountDocuments(
                t =>t.PreferredTagName == preferredName&&
                (t.Statue==TagReviewedStatue.ToBeReviewed||t.Statue==TagReviewedStatue.Passed)
            );
            return tagReviewedCount > 0;
        }

        public async Task<TagReviewed> GetTagReviewedAsync(string tagReviewedId)
        {
            var tag=await _context.TagRevieweds.FindAsync(t => t.Id == tagReviewedId);
            return tag.First();
        }

        public async Task<(bool result,TagReviewed? entity)> PassReviewedTagAsync(string tagReviewedId)
        {
            var reviewedTag=_context.TagRevieweds.Find(t =>t.Id==tagReviewedId).First();
            if (reviewedTag?.Statue == TagReviewedStatue.ToBeReviewed)
            {
                var update = reviewedTag.SetPassed();
                var result = await _context.TagRevieweds.UpdateOneAsync(_session, t => t.Id == tagReviewedId, update);
                return (result.ModifiedCount == 1, reviewedTag);
            }

            return (false,null);
        }

        public async Task<(bool result, TagReviewed? entity)> RefuseReviewedTagAsync(string tagReviewedId)
        {
            var reviewedTag = _context.TagRevieweds.Find(t => t.Id == tagReviewedId).First();
            if (reviewedTag?.Statue == TagReviewedStatue.ToBeReviewed)
            {
                var update = reviewedTag.SetRefused();
                var result = await _context.TagRevieweds.UpdateOneAsync(_session, t => t.Id == tagReviewedId, update);
                return (result.ModifiedCount == 1, reviewedTag);
            }

            return (false, null);
        }
    }
}
