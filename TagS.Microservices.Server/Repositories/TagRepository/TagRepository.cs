namespace TagS.Microservices.Server.Repositories.TagRepository
{
    public class TagRepository:ITagRepository
    {
        private readonly IMediator _mediator;
        private readonly TagSMongoDBContext _context;
        public TagRepository(IMediator mediator,TagSMongoDBContext context)
        {
            _mediator = mediator;
            _context=context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task AddAsync(Tag tag)
        {
            await _context.Tags.InsertOneAsync(tag);
            await _mediator.Send(new AddTagDomainEvent(new TagWithReferrer(tag.Id,tag.PreferredTagName,tag.TagDetail,tag.Synonyms,null)));
        }

        public async Task AddAsync(IEnumerable<Tag> tags)
        {
            await _context.Tags.InsertManyAsync(tags);
            foreach (var tag in tags)
            {
                await _mediator.Send(new AddTagDomainEvent(new TagWithReferrer(tag.Id, tag.PreferredTagName, tag.TagDetail, tag.Synonyms, null)));
            }
        }

        public Task<BulkWriteResult<Tag>> BulkWriteAsync(IEnumerable<Tag> tags)
        {
            var tagUpdateModel=tags.Select(t=>new UpdateOneModel<Tag>(new ExpressionFilterDefinition<Tag>(tag => tag.Id == t.Id),t.ToBsonDocument())).ToList();
            return _context.Tags.BulkWriteAsync(tagUpdateModel);
        }
        //TODO soft delete?
        public async Task<DeleteResult> DeleteAsync(string tagId)
        {
            var result=await _context.Tags.DeleteOneAsync(t=>t.Id==tagId);
            await _mediator.Send(new DeleteTagDomainEvent(tagId));
            return result;
        }

        public async Task<DeleteResult> DeleteAsync(IEnumerable<string> tagIds)
        {
            var result= await _context.Tags.DeleteManyAsync(t=>tagIds.Contains(t.Id));
            foreach (var tagId in tagIds)
            {
                await _mediator.Send(new DeleteTagDomainEvent(tagId));
            }
            return result;
        }

        public bool Existed(string tagId)
        {
            return _context.Tags.FindSync(t=>t.Id==tagId).Any();
        }

        public bool ExistedPreferredName(string preferredTagName)
        {
            return _context.Tags.FindSync(t => t.PreferredTagName == preferredTagName).Any();
        }

        public async Task<Tag> GetTagAsync(string tagId)
        {
            var tag=await _context.Tags.FindAsync(t=>t.Id==tagId);
            return tag.FirstOrDefault();
        }

        public Task<UpdateResult> UpdateAsync(Tag tag)
        {
            return _context.Tags.UpdateOneAsync(t => t.Id == tag.Id, tag.ToBsonDocument());
        }

        public Task<UpdateResult> UpdateAsync(IEnumerable<string> tagIds,UpdateDefinition<Tag> updateDefinition)
        {
            return _context.Tags.UpdateManyAsync(t => tagIds.Contains(t.Id), updateDefinition);
        }

        private bool IsSynonymOfExistedTag(string synonym)
        {
            var tagWithSynonym=_context.Tags.FindSync(t=>t.Synonyms.Contains(synonym));
            return tagWithSynonym.Any();
        }
    }
}
