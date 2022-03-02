namespace TagS.Microservices.Client.DomainEvents
{
    public class AddTagDomainEvent : INotification
    {
        public IReferrer Referrer { get;private set; }
        public string TagId { get;private set; }
        public AddTagDomainEvent(IReferrer referrer, string tagId)
        {
            Referrer = referrer;
            TagId = tagId;
        }
    }
}
