namespace TagS.Microservices.Server.Queries.TagReviewedQueries
{
    public class TagReviewedQueries : ITagReviewedQueries
    {
        private readonly TagSMongoDBContext _context;
        public TagReviewedQueries(TagSMongoDBContext tagSMongoDBContext, IClientSessionHandle session)
        {
            _context = tagSMongoDBContext;
        }
        public async Task<IEnumerable<TagReviewedDTO>> GetTobeReviewedTagsAsync()
        {
            var tags = await _context.TagRevieweds.FindAsync(t => t.Statue == TagReviewedStatue.ToBeReviewed);
            return tags.ToList().Select(t => MapTagReviewedToTagReviewedDTO(t));
        }

        private TagReviewedDTO MapTagReviewedToTagReviewedDTO(TagReviewed tagReviewed)
        {
            return new TagReviewedDTO(
                tagReviewed.Id,
                tagReviewed.PreferredTagName,
                tagReviewed.TagDetail,
                tagReviewed.PreviousTagId,
                tagReviewed.Ancestors,
                tagReviewed.UserId,
                tagReviewed.CreateTime
            );
        }
    }
}
