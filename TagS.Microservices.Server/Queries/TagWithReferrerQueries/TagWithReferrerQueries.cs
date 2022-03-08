namespace TagS.Microservices.Server.Queries.TagWithReferrerQueries
{
    public class TagWithReferrerQueries : ITagWithReferrerQueries
    {
        private readonly TagSMongoDBContext _context;
        public TagWithReferrerQueries(TagSMongoDBContext tagSMongoDBContext)
        {
            _context = tagSMongoDBContext;
        }
        public async Task<IEnumerable<TReferrer>> GetReferrersOfTagAsync<TReferrer>(string tagId, Func<TReferrer, bool> predicate) where TReferrer : IReferrer
        {
            var referrers = _context.TagWithReferrers.AsQueryable().First(t => t.Id == tagId).Referrers.OfType<TReferrer>().Where(predicate);
            return await Task.FromResult(referrers);
        }
    }
}
