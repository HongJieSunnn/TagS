namespace TagS.Microservices.Server.DomainEventHandlers
{
    public class DeleteTagDomainEventHandler : INotificationHandler<DeleteTagDomainEvent>
    {
        private readonly ITagWithReferrerRepository _tagWithReferrerRepository;
        public DeleteTagDomainEventHandler(ITagWithReferrerRepository tagWithReferrerRepository)
        {
            _tagWithReferrerRepository = tagWithReferrerRepository;

        }

        public async Task Handle(DeleteTagDomainEvent notification, CancellationToken cancellationToken)
        {
            await _tagWithReferrerRepository.DeleteAsync(notification.TagId);
            //TODO notify Tag clients.
        }
    }
}
