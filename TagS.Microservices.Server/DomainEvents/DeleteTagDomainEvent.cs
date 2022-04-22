namespace TagS.Microservices.Server.DomainEvents
{
    public class DeleteTagDomainEvent:INotification
    {
        public string TagId { get;init; }
        public DeleteTagDomainEvent(string tagId)
        {
            TagId=tagId;
        }
    }
}
