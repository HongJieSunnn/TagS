namespace TagS.Microservices.Server.DomainEvents
{
    internal class DeleteTagDomainEvent:IRequest
    {
        public string TagId { get; set; }
        public DeleteTagDomainEvent(string tagId)
        {
            TagId=tagId;
        }
    }
}
