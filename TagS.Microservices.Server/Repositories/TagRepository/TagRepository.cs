namespace TagS.Microservices.Server.Repositories.TagRepository
{
    public class TagRepository : ITagRepository
    {
        private readonly IMediator _mediator;
        private readonly TagSMongoDBContext _context;
        private readonly IClientSessionHandle _session;
        public TagRepository(IMediator mediator, TagSMongoDBContext context, IClientSessionHandle session)
        {
            _mediator = mediator;
            _context = context;
            _session = session;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task AddAsync(Tag tag)
        {
            await _context.Tags!.InsertOneAsync(_session, tag);
            await _mediator.Publish(new AddTagDomainEvent(new TagWithReferrer(tag.Id, tag.PreferredTagName, tag.TagDetail, tag.Synonyms.ToList(), null, tag.CreateTime, tag.PreviousTagId, tag.Ancestors?.ToList())));
        }

        public Task<BulkWriteResult<Tag>> BulkWriteAsync(IEnumerable<Tag> tags)
        {
            var tagUpdateModel = tags.Select(t => new UpdateOneModel<Tag>(new ExpressionFilterDefinition<Tag>(tag => tag.Id == t.Id), t.ToBsonDocument())).ToList();
            return _context.Tags!.BulkWriteAsync(tagUpdateModel);
        }
        //TODO soft delete?
        public async Task<UpdateResult> DeleteAsync(string tagId)
        {
            var tag = _context.Tags.Find(t => t.Id == tagId).First();
            var update = tag.SetDeleted();
            var result = await _context.Tags.UpdateOneAsync(_session, t => t.Id == tagId, update);
            return result;
        }

        public async Task<UpdateResult> DeleteAsync(IEnumerable<string> tagIds)
        {
            var tags = _context.Tags.Find(t => tagIds.Contains(t.Id)).ToList();
            UpdateDefinition<Tag>? update = null;
            foreach (var tag in tags)
            {
                update = tag.SetDeleted();
            }
            var result = await _context.Tags.UpdateManyAsync(_session, t => tagIds.Contains(t.Id), update);
            return result;
        }

        public bool Existed(string tagId)
        {
            return _context.Tags.FindSync(t => t.Id == tagId).Any();
        }

        public bool ExistedPreferredName(string preferredTagName)
        {
            return _context.Tags.FindSync(t => t.PreferredTagName == preferredTagName).Any();
        }

        public async Task<Tag> GetTagAsync(string tagId)
        {
            var tag = await _context.Tags.FindAsync(t => t.Id == tagId);
            return tag.FirstOrDefault();
        }

        public Task<ReplaceOneResult> UpdateAsync(Tag tag)
        {
            return _context.Tags.ReplaceOneAsync(_session, t => t.Id == tag.Id, tag);
        }

        public Task<UpdateResult> UpdateAsync(IEnumerable<string> tagIds, UpdateDefinition<Tag> updateDefinition)
        {
            return _context.Tags.UpdateManyAsync(_session, t => tagIds.Contains(t.Id), updateDefinition);
        }

        public Task<UpdateResult> UpdateAsync(string tagId, UpdateDefinition<Tag> updateDefinition)
        {
            return _context.Tags.UpdateOneAsync(_session, t => t.Id == tagId, updateDefinition);
        }

        private bool IsSynonymOfExistedTag(string synonym)
        {
            var tagWithSynonym = _context.Tags.FindSync(t => t.Synonyms.Contains(synonym));
            return tagWithSynonym.Any();
        }
    }
}
