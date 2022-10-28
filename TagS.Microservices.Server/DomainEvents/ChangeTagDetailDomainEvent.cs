namespace TagS.Microservices.Server.DomainEvents
{
    public class ChangeTagDetailDomainEvent : INotification
    {
        public string TagId { get; private set; }
        public string TagDetail { get; private set; }

        public ChangeTagDetailDomainEvent(string tagId, string tagDetail)
        {
            TagId = tagId;
            TagDetail = tagDetail;
        }
    }
}
