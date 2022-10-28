namespace TagS.Microservices.Server.Models
{
    public class TagReviewed : Entity<string>, IAggregateRoot
    {
        public string PreferredTagName { get; private set; }
        public string TagDetail { get; private set; }
        public string? PreviousTagId { get; private set; }
        public List<string>? Ancestors { get; private set; }
        public List<string>? Synonyms { get; set; }
        public string UserId { get; private set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; private set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? UpdateTime { get; private set; }
        public TagReviewedStatue Statue { get; private set; }

        [BsonConstructor]
        public TagReviewed(string preferredTagName, string tagDetail, string userId, DateTime createTime, string? previousTagId = null, List<string>? ancestors = null, List<string>? synonyms = null)
        {
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            UserId = userId;
            CreateTime = createTime;
            UpdateTime = null;
            PreviousTagId = previousTagId;
            Ancestors = ancestors;
            Synonyms = synonyms ?? new List<string>();
            Statue = TagReviewedStatue.ToBeReviewed;
        }

        public UpdateDefinition<TagReviewed> SetPassed()
        {
            Statue = TagReviewedStatue.Passed;
            UpdateTime = DateTime.Now;
            AddDomainEvent(new SetReviewedTagPassedDomainEvent(PreferredTagName, TagDetail, PreviousTagId, Ancestors, Synonyms));
            return Builders<TagReviewed>.Update.Set(t => t.Statue, Statue).Set(t => t.UpdateTime, UpdateTime);
        }

        public UpdateDefinition<TagReviewed> SetRefused()
        {
            Statue = TagReviewedStatue.Refused;
            UpdateTime = DateTime.Now;
            return Builders<TagReviewed>.Update.Set(t => t.Statue, Statue).Set(t => t.UpdateTime, UpdateTime);
        }
    }

    public enum TagReviewedStatue
    {
        ToBeReviewed = 0,
        Passed = 1,
        Refused = 2,
    }
}
