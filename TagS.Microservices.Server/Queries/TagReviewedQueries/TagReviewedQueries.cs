namespace TagS.Microservices.Server.Queries.TagReviewedQueries
{
    public class TagReviewedQueries : ITagReviewedQueries
    {
        private readonly TagSMongoDBContext _context;
        public TagReviewedQueries(TagSMongoDBContext tagSMongoDBContext)
        {
            _context = tagSMongoDBContext;
        }
        public async Task<IEnumerable<TagReviewed>> GetTobeReviewedTagsAsync()
        {
            var tags=await _context.TagReviedweds.FindAsync(t=>t.Statue==TagReviewedStatue.ToBeReviewed);
            return tags.ToList();
        }
    }
}
