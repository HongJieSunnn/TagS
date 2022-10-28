namespace TagS.Microservices.Server.DomainEvents
{
    public class RemoveSynonymDomainEvent : INotification
    {
        public string TagId { get; private set; }
        public string Synonym { get; private set; }
        public RemoveSynonymDomainEvent(string tagId, string synonym)
        {
            TagId = tagId;
            Synonym = synonym;
        }
    }
}
