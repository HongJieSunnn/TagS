namespace TagS.Microservices.Server.DomainEvents.TagReviewed
{
    public class SetReviewedTagPassedDomainEvent:INotification
    {
        public string PreferredTagName { get; private set; }
        public string TagDetail { get; private set; }
        public string? PreviousTagId { get; private set; }
        public string? FirstLevelTagId { get; private set; }
        public SetReviewedTagPassedDomainEvent(string preferredTagName,string tagDetail,string? previousTagId,string? firstLevelTagId)
        {
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            PreviousTagId = previousTagId;
            FirstLevelTagId = firstLevelTagId;
        }
    }
}
