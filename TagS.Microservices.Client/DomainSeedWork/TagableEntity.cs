namespace TagS.Microservices.Client.DomainSeedWork
{
    public abstract class TagableEntity : Entity
    {
        /// <summary>
        /// Initial in Derived TagableEntity Class.
        /// </summary>
        public List<TagSummary> Tags { get;private set; }

        public TagableEntity(List<TagSummary> tagSummaries)
        {
            Tags = tagSummaries;
        }

        public void AddTag(TagSummary tag)
        {
            Tags.Add(tag);
            var addEvent = new AddTagDomainEvent(ToReferrer(), tag.TagId);
            AddDomainEvent(addEvent);
        }

        public void UpdateTag(TagSummary tag)
        {
            var tagToUpdate=Tags.First(t => t.TagId == tag.TagId);
            tagToUpdate.Name = tag.Name;
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
}
