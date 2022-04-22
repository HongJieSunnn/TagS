namespace TagS.Microservices.Server.Models
{
    public class Tag:Entity<string>,IAggregateRoot
    {
        public string PreferredTagName { get;private set; }//unique name should be like Emotion:Happy,ProgramingLanguage:C#:C#10
        public string TagDetail { get; private set; }
        public string? PreviousTagId { get; private set; }

        [BsonRequired]
        [BsonElement("Ancestors")]
        private List<string>? _ancestors;
        public IReadOnlyCollection<string>? Ancestors =>_ancestors?.AsReadOnly();

        [BsonRequired]
        [BsonElement("Synonyms")]
        private List<string> _synonyms;
        public IReadOnlyCollection<string> Synonyms => _synonyms.AsReadOnly();

        [BsonRequired]
        [BsonElement("RelatedTagIds")]
        private List<string> _relatedTagIds;
        public IReadOnlyCollection<string> RelatedTagIds => _relatedTagIds.AsReadOnly();

        [BsonDateTimeOptions(Kind =DateTimeKind.Local)]
        public DateTime CreateTime { get;private set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? UpdateTime { get;private set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? DeleteTime { get; private set; }

        //TODO I don't know why [BsonConstructor] will cause error while GetCollection<Tag> by MongoDBContextBase.But it's useful in TagReviewed.
        public Tag(string? id, string preferredTagName, string tagDetail, string? previousTagId,List<string>? ancestors, List<string>? synonyms, List<string>? realatedTagIds)
        {
            Id = id??ObjectId.GenerateNewId().ToString();
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            PreviousTagId = previousTagId;
            _ancestors=ancestors;
            _synonyms = synonyms??new List<string>();
            _relatedTagIds = realatedTagIds??new List<string>();
            CreateTime = DateTime.Now;
        }

        //TODO modify the return value to UpdateDefinition<Tag>
        public void AddSynonym(string synonym)
        {
            if(!Synonyms.Contains(synonym))
                _synonyms.Add(synonym);
            SetUpdateTime();
            AddDomainEvent(new AddSynonymDomainEvent(this.Id!, synonym));
        }

        public void RemoveSynonym(string synonym)
        {
            if (Synonyms.Contains(synonym))
                _synonyms.Remove(synonym);
            SetUpdateTime();
            AddDomainEvent(new RemoveSynonymDomainEvent(this.Id!, synonym));
        }

        public void AddRelatedTagId(string tagId)
        {
            if (!RelatedTagIds.Contains(tagId))
                _relatedTagIds.Add(tagId);
            SetUpdateTime();
        }

        public void RemoveRelatedTagId(string tagId)
        {
            if (RelatedTagIds.Contains(tagId))
                _relatedTagIds.Remove(tagId);
            SetUpdateTime();
        }

        public void ChangeTagDetail(string tagDetail)
        {
            TagDetail = tagDetail;
            AddDomainEvent(new ChangeTagDetailDomainEvent(this.Id!, tagDetail));
        }

        public UpdateDefinition<Tag> SetDeleted()
        {
            DeleteTime = DateTime.Now;
            //DomainEvents
            return Builders<Tag>.Update.Set(t => t.DeleteTime, DeleteTime);
        }

        private void SetUpdateTime()
        {
            UpdateTime = DateTime.Now;
        }
    }
}
