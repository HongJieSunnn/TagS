namespace TagS.Microservices.Client.DomainEventHandlers
{
    public class RemoveTagDomainEventHandler : INotificationHandler<RemoveTagDomainEvent>
    {
        private readonly IRepository<IAggregateRoot>? _repository;
        private readonly IIntegrationEventService _integrationEventService;
        private readonly ILogger<RemoveTagDomainEventHandler> _logger;
        public RemoveTagDomainEventHandler(IIntegrationEventService integrationEventService, ILogger<RemoveTagDomainEventHandler> logger, IRepository<IAggregateRoot>? repository = null)
        {
            _repository = repository;
            _integrationEventService = integrationEventService;
            _logger = logger;
        }
        public async Task Handle(RemoveTagDomainEvent notification, CancellationToken cancellationToken)
        {
            var removeTagIntegrationEvent = new RemoveReferrerToTagServerIntegrationEvent(notification.Referrer, notification.TagId);
            await _integrationEventService.AddAndSaveEventAsync(removeTagIntegrationEvent);


            if (_integrationEventService.GetType() == typeof(ITagIntegrationEventService))
                await (_integrationEventService as ITagIntegrationEventService)!.PublishEventAsync(removeTagIntegrationEvent.Id);
        }
    }
}
