namespace TagS.Microservices.Server.DomainEvents
{
    public class DeleteTagDomainEvent:INotification
    {
        public string TagId { get; set; }
        public DeleteTagDomainEvent(string tagId)
        {
            TagId=tagId;
        }
    }
}
