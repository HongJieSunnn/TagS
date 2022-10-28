namespace TagS.Microservices.Server.DomainEventHandlers
{
    public class RemoveSynonymDomainEventHandler : INotificationHandler<RemoveSynonymDomainEvent>
    {
        private readonly ITagWithReferrerRepository _tagWithReferrerRepository;
        public RemoveSynonymDomainEventHandler(ITagWithReferrerRepository tagWithReferrerRepository)
        {
            _tagWithReferrerRepository = tagWithReferrerRepository;

        }
        public async Task Handle(RemoveSynonymDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
