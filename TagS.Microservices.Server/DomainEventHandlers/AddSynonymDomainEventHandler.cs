namespace TagS.Microservices.Server.DomainEventHandlers
{
    internal class AddSynonymDomainEventHandler : INotificationHandler<AddSynonymDomainEvent>
    {
        private readonly ITagWithReferrerRepository _tagWithReferrerRepository;
        public AddSynonymDomainEventHandler(ITagWithReferrerRepository tagWithReferrerRepository)
        {
            _tagWithReferrerRepository= tagWithReferrerRepository;
        }
        public async Task Handle(AddSynonymDomainEvent notification, CancellationToken cancellationToken)
        {
            await _tagWithReferrerRepository.AddSynonymAsync(notification.TagId, notification.Synonym);
        }
    }
}
