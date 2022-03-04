namespace TagS.Microservices.Server.DomainEvents
{
    public class AddTagDomainEvent:IRequest
    {
        public TagWithReferrer TagWithReferrer { get; private set; }
        public AddTagDomainEvent(TagWithReferrer tagWithReferrer)
        {
            TagWithReferrer = tagWithReferrer;
        }
    }
}
