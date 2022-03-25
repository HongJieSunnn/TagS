using DomainSeedWork.Abstractions;

namespace TagS.Microservices.Client.DomainSeedWork
{
    public abstract class TagableEntity<TId> : Entity<TId>
        where TId : IEquatable<TId>, IComparable<TId>
    {
        /// <summary>
        /// Initial in Derived TagableEntity Class.
        /// </summary>
        public List<TagSummary> Tags { get; set; }

        public TagableEntity(List<TagSummary> tagSummaries)
        {
            Tags = tagSummaries;
        }

        public void AddDomainEventForAddingTag(TagSummary tag)
        {
            var addEvent = new AddTagDomainEvent(ToReferrer(), tag.TagId);
            AddDomainEvent(addEvent);
        }

        public void AddTag(TagSummary tag)
        {
            Tags.Add(tag);
            AddDomainEventForAddingTag(tag);
        }

        public void UpdateTag(TagSummary tag)
        {
            var tagToUpdate = Tags.First(t => t.TagId == tag.TagId);
            tagToUpdate.TagName = tag.TagName;
        }

        public void RemoveTag(TagSummary tag)
        {
            Tags.Remove(tag);
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
        public List<TagSummary<TId,TEntity>> Tags { get; set; }

        public TagableEntity(List<TagSummary<TId, TEntity>> tagSummaries)
        {
            Tags = tagSummaries;
        }

        public void AddDomainEventForAddingTag(TagSummary<TId, TEntity> tag)
        {
            var addEvent = new AddTagDomainEvent(ToReferrer(), tag.TagId);
            AddDomainEvent(addEvent);
        }

        public void AddTag(TagSummary<TId, TEntity> tag)
        {
            Tags.Add(tag);
            AddDomainEventForAddingTag(tag);
        }

        public void UpdateTag(TagSummary<TId, TEntity> tag)
        {
            var tagToUpdate = Tags.First(t => t.TagId == tag.TagId);
            tagToUpdate.TagName = tag.TagName;
        }

        public void RemoveTag(TagSummary<TId, TEntity> tag)
        {
            Tags.Remove(tag);
            var removeEvent = new RemoveTagDomainEvent(ToReferrer(), tag.TagId);
            AddDomainEvent(removeEvent);
        }

        protected abstract IReferrer ToReferrer();
    }
}
