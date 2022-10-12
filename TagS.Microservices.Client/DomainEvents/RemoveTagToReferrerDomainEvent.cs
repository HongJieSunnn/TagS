namespace TagS.Microservices.Client.DomainEvents
{
    public class RemoveTagToReferrerDomainEvent : INotification
    {
        public IReferrer Referrer { get; private set; }
        public string TagId { get; private set; }
        public RemoveTagToReferrerDomainEvent(IReferrer referrer, string tagId)
        {
            Referrer = referrer;
            TagId = tagId;
        }
    }
}
