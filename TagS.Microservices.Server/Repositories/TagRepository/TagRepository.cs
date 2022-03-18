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
            await _mediator.Publish(new AddTagDomainEvent(new TagWithReferrer(tag.Id, tag.PreferredTagName, tag.TagDetail, tag.Synonyms, null, tag.CreateTime)));
            await SendAddTagDomainEventForNextTagsAsync(tag);
        }

        public async Task AddAsync(IEnumerable<Tag> tags)
        {
            await _context.Tags.InsertManyAsync(tags);
            foreach (var tag in tags)
            {
                await _mediator.Publish(new AddTagDomainEvent(new TagWithReferrer(tag.Id, tag.PreferredTagName, tag.TagDetail, tag.Synonyms, null, tag.CreateTime)));
                await SendAddTagDomainEventForNextTagsAsync(tag);
            }
        }

        /// <summary>
        /// While we add a tag with nextTags,we add nextTags to TagWithReferrerCollection while publish the domainEvents added in AddNextTag method.
        /// But when the nextTags also have nextTags,the domainEvents belong to nextTag.So we can not publish them by first level tag.
        /// So we should publish them manually.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private async Task SendAddTagDomainEventForNextTagsAsync(Tag tag)
        {
            if (tag.NextTags.Count == 0)
                return;
            foreach (var nextTag in tag.NextTags)
            {
                if(nextTag.DomainEvents is not null)
                {
                    foreach (var domainEvent in nextTag.DomainEvents)
                    {
                        await _mediator.Publish(domainEvent);
                    }
                    await SendAddTagDomainEventForNextTagsAsync(nextTag);
                }
            }
        }

        private Task SendAddTagDomainEventAsync(Tag tag)
        {
            Task<object?>[] AddTagDomainEventTasks = new Task<object?>[1 + tag.NextTags.Count];

            AddTagDomainEventTasks[0] = _mediator.Send(new AddTagDomainEvent(new TagWithReferrer(tag.Id, tag.PreferredTagName, tag.TagDetail, tag.Synonyms, null,tag.CreateTime)));
            for (int i = 0; i < tag.NextTags.Count; ++i)
            {
                AddTagDomainEventTasks[i + 1] = _mediator.Send(new AddTagDomainEvent(
                    new TagWithReferrer(tag.NextTags[i].Id, tag.NextTags[i].PreferredTagName, tag.NextTags[i].TagDetail, tag.NextTags[i].Synonyms, null, tag.NextTags[i].CreateTime))
                );
            }

            return Task.WhenAll(AddTagDomainEventTasks);
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
