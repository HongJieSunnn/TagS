namespace TagS.Microservices.Server.DomainEvents.TagReviewed
{
    public class SetReviewedTagPassedDomainEvent:INotification
    {
        public string PreferredTagName { get; private set; }
        public string TagDetail { get; private set; }
        public string? PreviousTagId { get; private set; }
        public SetReviewedTagPassedDomainEvent(string preferredTagName,string tagDetail,string? previousTagId)
        {
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            PreviousTagId = previousTagId;
        }
    }
}
