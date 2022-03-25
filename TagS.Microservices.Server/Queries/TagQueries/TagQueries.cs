namespace TagS.Microservices.Server.Queries.TagQueries
{
    public class TagQueries : ITagQueries
    {
        private readonly TagSMongoDBContext _context;
        public TagQueries(TagSMongoDBContext tagSMongoDBContext)
        {
            _context = tagSMongoDBContext;
        }

        public async Task<IEnumerable<TagDTO>> GetAllTagsAsync()
        {
            var tags =await _context.Tags.AsQueryable().ToListAsync();
            return tags.Select(t=>MapTagToTagDTO(t));
        }

        public async Task<IEnumerable<TagDTO>> GetAllFirstLevelTagsAsync()
        {
            var tags = await _context.Tags.FindAsync(t=>t.PreviousTagId==null);
            var tagDTOs=tags.ToList().Select(t=>MapTagToTagDTO(t));
            return tagDTOs;
        }

        public async Task<IEnumerable<TagDTO>> GetNextTagsAsync(string tagId)
        {
            var tags=await _context.Tags.FindAsync(t=>t.PreviousTagId== tagId);
            var tagDTOs=tags.ToList().Select(t=>MapTagToTagDTO(t));
            return tagDTOs;
        }

        public async Task<TagDTO> GetTagByPreferredNameAsync(string preferredName)
        {
            var tag = await _context.Tags.FindAsync(t=>t.PreferredTagName== preferredName);
            return MapTagToTagDTO(tag.First());
        }

        public async Task<TagDTO> GetTagBySynonymAsync(string synonym)
        {
            var filter = Builders<Tag>.Filter.Eq("Synonyms", synonym);
            var tag = await _context.Tags.FindAsync(filter);//Call exception.
            return MapTagToTagDTO(tag.First());
        }

        private TagDTO MapTagToTagDTO(Tag tag)
        {
            return new TagDTO(
                tag.Id, 
                tag.PreferredTagName, 
                tag.TagDetail, 
                tag.PreviousTagId, 
                tag.Ancestors?.ToList(), 
                tag.Synonyms.ToList(), 
                tag.RelatedTagIds.ToList(), 
                tag.CreateTime
            );
        }
    }
}
