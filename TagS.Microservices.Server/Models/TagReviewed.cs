namespace TagS.Microservices.Server.Models
{
    public class TagReviewed:Entity<string>,IAggregateRoot
    {
        public string PreferredTagName { get; private set; }
        public string TagDetail { get; private set; }
        public string? PreviousTagId { get; private set; }
        public Guid UserId { get;private set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreateTime { get;private set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime UpdateTime { get;private set; }
        public TagReviewedStatue Statue { get;private set; }

        [BsonConstructor]
        public TagReviewed(string preferredTagName,string tagDetail,Guid userId,DateTime createTime,string? previousTagId=null)
        {
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            UserId=userId;
            CreateTime = createTime;
            UpdateTime = default(DateTime);
            PreviousTagId = previousTagId;
            Statue = TagReviewedStatue.ToBeReviewed;
        }

        public void SetPassed()
        {
            Statue = TagReviewedStatue.Passed;
            UpdateTime=DateTime.Now;
            AddDomainEvent(new SetReviewedTagPassedDomainEvent(PreferredTagName,TagDetail,PreviousTagId));
        }

        public void SetRefused()
        {
            Statue = TagReviewedStatue.Refused;
            UpdateTime = DateTime.Now;
        }
    }

    public enum TagReviewedStatue
    {
        ToBeReviewed=0,
        Passed=1,
        Refused=2,
    }
}
