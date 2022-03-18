namespace TagS.Microservices.Server.Models
{
    public class Tag:Entity<string>,IAggregateRoot
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public override string Id { get => base.Id; set => base.Id = value; }
        public string PreferredTagName { get;private set; }//unique name should be like Emotion:Happy,ProgramingLanguage:C#:C#10
        public string TagDetail { get; private set; }
        public string? PreviousTagId { get; private set; }
        public List<string> Synonyms { get; private set; }
        public List<string> RelatedTagIds { get; private set; }
        public List<Tag> NextTags { get; private set; }
        [BsonDateTimeOptions(Kind =DateTimeKind.Local)]
        public DateTime CreateTime { get;private set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? UpdateTime { get;private set; }

        //TODO I don't know why [BsonConstructor] will cause error while GetCollection<Tag> by MongoDBContextBase.But it's useful in TagReviewed.
        public Tag(string? id, string preferredTagName, string tagDetail, string? previousTagId, List<string>? synonyms, List<string>? realatedTagIds, List<Tag>? nextTags)
        {
            Id = id??ObjectId.GenerateNewId().ToString();
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            PreviousTagId = previousTagId;
            Synonyms = synonyms??new List<string>();
            RelatedTagIds = realatedTagIds??new List<string>();
            NextTags = nextTags??new List<Tag>();
            CreateTime = DateTime.Now;
        }

        public void AddSynonym(string synonym)
        {
            if(!Synonyms.Contains(synonym))
                Synonyms.Add(synonym);
            SetUpdateTime();
            AddDomainEvent(new AddSynonymDomainEvent(this.Id, synonym));
        }

        public void RemoveSynonym(string synonym)
        {
            if (Synonyms.Contains(synonym))
                Synonyms.Remove(synonym);
            SetUpdateTime();
            AddDomainEvent(new RemoveSynonymDomainEvent(this.Id, synonym));
        }

        public void AddRelatedTagId(string tagId)
        {
            if (!RelatedTagIds.Contains(tagId))
                RelatedTagIds.Add(tagId);
            SetUpdateTime();
        }

        public void RemoveRelatedTagId(string tagId)
        {
            if (RelatedTagIds.Contains(tagId))
                RelatedTagIds.Remove(tagId);
            SetUpdateTime();
        }

        public void AddNextTag(Tag tag)
        {
            if(NextTags.FirstOrDefault(t=>t.PreferredTagName==tag.PreferredTagName) is null)
            {
                if (tag.PreviousTagId is null)
                    tag.PreviousTagId = this.Id;
                NextTags.Add(tag);
                SetUpdateTime();
                //While we add a new tag which is not first level tag we should also send this domainEvent.
                AddDomainEvent(new AddTagDomainEvent(new TagWithReferrer(tag.Id, tag.PreferredTagName, tag.TagDetail, tag.Synonyms, null,tag.CreateTime)));
            }
        }

        public void RemoveNextTag(Tag tag)
        {
            if (NextTags.Contains(tag))
            {
                NextTags.Remove(tag);
                AddDomainEvent(new DeleteTagDomainEvent(tag.Id));
            }
        }

        public void ChangeTagDetail(string tagDetail)
        {
            TagDetail = tagDetail;
            AddDomainEvent(new ChangeTagDetailDomainEvent(this.Id, tagDetail));
        }

        private void SetUpdateTime()
        {
            UpdateTime = DateTime.Now;
        }
    }
}
