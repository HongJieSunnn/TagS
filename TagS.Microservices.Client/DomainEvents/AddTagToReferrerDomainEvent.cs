namespace TagS.Microservices.Client.DomainEvents
{
    public class AddTagToReferrerDomainEvent : INotification
    {
        public IReferrer Referrer { get;private set; }
        public string TagId { get;private set; }
        public AddTagToReferrerDomainEvent(IReferrer referrer, string tagId)
        {
            Referrer = referrer;
            TagId = tagId;
        }
    }
}
