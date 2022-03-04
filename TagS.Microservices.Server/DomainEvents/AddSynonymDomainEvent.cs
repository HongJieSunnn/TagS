namespace TagS.Microservices.Server.DomainEvents
{
    public class AddSynonymDomainEvent:INotification
    {
        public string TagId { get;private set; }
        public string Synonym { get;private set; }
        public AddSynonymDomainEvent(string tagId,string synonym)
        {
            TagId = tagId;
            Synonym = synonym;
        }
    }
}
