namespace TagS.Microservices.Server.Models
{
    public class Tag:Entity<string>,IAggregateRoot
    {
        public string PreferredTagName { get;private set; }//unique name should be like Emotion:Happy,ProgramingLanguage:C#:C#10
        public string TagDetail { get; private set; }
        public string? PreviousTagId { get; private set; }
        public List<string> Synonyms { get; private set; }
        public List<string> RelatedTagIds { get; private set; }
        public List<Tag> NextTags { get; private set; }

        [BsonConstructor]
        public Tag(string id, string preferredTagName, string tagDetail, string previousTagId, List<string> synonyms, List<string> realatedTagIds, List<Tag> nextTags)
        {
            Id = id;
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            PreviousTagId = previousTagId;
            Synonyms = synonyms;
            RelatedTagIds = realatedTagIds;
            NextTags = nextTags??new List<Tag>();
        }

        public void AddSynonym(string synonym)
        {
            if(!Synonyms.Contains(synonym))
                Synonyms.Add(synonym);
            AddDomainEvent(new AddSynonymDomainEvent(this.Id, synonym));
        }

        public void RemoveSynonym(string synonym)
        {
            if (Synonyms.Contains(synonym))
                Synonyms.Remove(synonym);
            AddDomainEvent(new RemoveSynonymDomainEvent(this.Id, synonym));
        }

        public void AddRelatedTagId(string tagId)
        {
            if (!RelatedTagIds.Contains(tagId))
                RelatedTagIds.Add(tagId);
        }

        public void RemoveRelatedTagId(string tagId)
        {
            if (RelatedTagIds.Contains(tagId))
                RelatedTagIds.Remove(tagId);
        }

        public void AddNextTag(Tag tag)
        {
            if(!NextTags.Contains(tag))
                NextTags.Add(tag);
        }

        public void RemoveNextTag(Tag tag)
        {
            if (NextTags.Contains(tag))
                NextTags.Remove(tag);
        }

        public void ChangeTagDetail(string tagDetail)
        {
            TagDetail = tagDetail;
            AddDomainEvent(new ChangeTagDetailDomainEvent(this.Id, tagDetail));
        }
    }
}
