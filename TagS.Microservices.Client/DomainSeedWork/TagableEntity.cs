using DomainSeedWork.Abstractions;

namespace TagS.Microservices.Client.DomainSeedWork
{
    public abstract class TagableEntity<TId> : Entity<TId>
        where TId : IEquatable<TId>, IComparable<TId>
    {
        /// <summary>
        /// Initial in Derived TagableEntity Class.
        /// </summary>
        [BsonRequired]
        [BsonElement("Tags")]
        private List<TagSummary> _tags { get; set; }

        public IReadOnlyCollection<TagSummary> Tags => _tags;
        public TagableEntity(List<TagSummary> tagSummaries)
        {
            _tags = tagSummaries;
        }

        public void AddTag(TagSummary tag)
        {
            _tags.Add(tag);
            AddDomainEventForAddingTag(tag);
        }

        public void AddDomainEventForAddingTag(TagSummary tag)
        {
            var addEvent = new AddTagDomainEvent(ToReferrer(), tag.TagId);
            AddDomainEvent(addEvent);
        }

        public void UpdateTag(TagSummary tag)
        {
            var tagToUpdate = Tags.First(t => t.TagId == tag.TagId);
            tagToUpdate.TagName = tag.TagName;
        }

        public void RemoveTag(TagSummary tag)
        {
            _tags.Remove(tag);
            AddDomainEventForRemovingTag(tag);
        }

        public void AddDomainEventForRemovingTag(TagSummary tag)
        {
            var removeEvent = new RemoveTagDomainEvent(ToReferrer(), tag.TagId);
            AddDomainEvent(removeEvent);
        }

        /// <summary>
        /// To identify how to change entity to the referrer so that store in TagWithReferrer Collection.
        /// </summary>
        /// <returns></returns>
        protected abstract IReferrer ToReferrer();
    }

    /// <summary>
    /// TagableEntity<TEntity> is used for SQLEntity.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class TagableEntity<TId,TEntity> : Entity<TId>
        where TEntity: TagableEntity<TId, TEntity>
        where TId : IEquatable<TId>, IComparable<TId>
    {
        public List<TagSummary<TId,TEntity>> Tags { get; set; }//2022.4.20 I don't modify to private field,I'm afraid there will call errors after modified.But it's not corresponding.

        public TagableEntity(List<TagSummary<TId, TEntity>> tagSummaries)
        {
            Tags = tagSummaries;
        }

        public void AddTag(TagSummary<TId, TEntity> tag)
        {
            Tags.Add(tag);
            AddDomainEventForAddingTag(tag);
        }

        public void AddDomainEventForAddingTag(TagSummary<TId, TEntity> tag)
        {
            var addEvent = new AddTagDomainEvent(ToReferrer(), tag.TagId);
            AddDomainEvent(addEvent);
        }

        public void UpdateTag(TagSummary<TId, TEntity> tag)
        {
            var tagToUpdate = Tags.First(t => t.TagId == tag.TagId);
            tagToUpdate.TagName = tag.TagName;
        }

        public void RemoveTag(TagSummary<TId, TEntity> tag)
        {
            Tags.Remove(tag);
            AddDomainEventForRemovingTag(tag);
        }

        public void AddDomainEventForRemovingTag(TagSummary<TId, TEntity> tag)
        {
            var removeEvent = new RemoveTagDomainEvent(ToReferrer(), tag.TagId);
            AddDomainEvent(removeEvent);
        }

        public abstract IReferrer ToReferrer();
    }
}
