namespace TagS.Microservices.Server.DomainEventHandlers
{
    public class ChangeTagDetailDomainEventHandler : INotificationHandler<ChangeTagDetailDomainEvent>
    {
        private readonly ITagWithReferrerRepository _tagWithReferrerRepository;
        public ChangeTagDetailDomainEventHandler(ITagWithReferrerRepository tagWithReferrerRepository)
        {
            _tagWithReferrerRepository = tagWithReferrerRepository;

        }
        public async Task Handle(ChangeTagDetailDomainEvent notification, CancellationToken cancellationToken)
        {
            await _tagWithReferrerRepository.ChangeTagDetailAsync(notification.TagId,notification.TagDetail);
        }
    }
}
