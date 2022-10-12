namespace TagS.Microservices.Client.DomainEventHandlers
{
    public class RemoveTagToReferrerDomainEventHandler : INotificationHandler<RemoveTagToReferrerDomainEvent>
    {
        private readonly IRepository<IAggregateRoot>? _repository;
        private readonly IIntegrationEventService _integrationEventService;
        private readonly ILogger<RemoveTagToReferrerDomainEventHandler> _logger;
        public RemoveTagToReferrerDomainEventHandler(IIntegrationEventService integrationEventService, ILogger<RemoveTagToReferrerDomainEventHandler> logger, IRepository<IAggregateRoot>? repository = null)
        {
            _repository = repository;
            _integrationEventService = integrationEventService;
            _logger = logger;
        }
        public async Task Handle(RemoveTagToReferrerDomainEvent notification, CancellationToken cancellationToken)
        {
            var removeTagIntegrationEvent = new RemoveReferrerToTagServerIntegrationEvent(notification.Referrer, notification.TagId);
            await _integrationEventService.SaveEventAsync(removeTagIntegrationEvent);
        }
    }
}
