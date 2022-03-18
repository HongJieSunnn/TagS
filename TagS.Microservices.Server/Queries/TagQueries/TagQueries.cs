namespace TagS.Microservices.Server.Queries.TagQueries
{
    public class TagQueries : ITagQueries
    {
        private readonly TagSMongoDBContext _context;
        public TagQueries(TagSMongoDBContext tagSMongoDBContext)
        {
            _context = tagSMongoDBContext;
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            var tags = await _context.Tags.FindAsync(t => t.Id != null);
            return tags.ToList();
        }

        public async Task<IEnumerable<Tag>> GetAllFirstLevelTagsAsync()
        {
            var tags = await _context.Tags.FindAsync(t => t.Id != null&&t.PreviousTagId==null);
            return tags.ToList();
        }

        public async Task<IEnumerable<Tag>> GetNextTagsAsync(string objectId)
        {
            var nextTags = await _context.Tags.FindAsync(t => t.Id == objectId);
            return nextTags.First().NextTags;
        }

        public async Task<Tag> GetTagByPreferredNameAsync(string preferredName)
        {
            var tag = await _context.Tags.FindAsync(t => t.PreferredTagName == preferredName);
            return tag.First();
        }

        public async Task<Tag> GetTagBySynonymAsync(string synonym)
        {
            var tag = await _context.Tags.FindAsync(t => t.Synonyms.Contains(synonym));
            return tag.First();
        }
    }
}
