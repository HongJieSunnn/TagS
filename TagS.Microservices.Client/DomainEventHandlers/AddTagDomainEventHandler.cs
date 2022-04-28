namespace TagS.Microservices.Client.DomainEventHandlers
{
    public class AddTagDomainEventHandler : INotificationHandler<AddTagDomainEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;
        private readonly ILogger<AddTagDomainEventHandler> _logger;
        public AddTagDomainEventHandler(IIntegrationEventService integrationEventService, ILogger<AddTagDomainEventHandler> logger)
        {
            _integrationEventService = integrationEventService;
            _logger = logger;
        }
        public async Task Handle(AddTagDomainEvent notification, CancellationToken cancellationToken)
        {
            var addTagIntegrationEvent = new AddReferrerToTagServerIntegrationEvent(notification.Referrer, notification.TagId);
            await _integrationEventService.SaveEventAsync(addTagIntegrationEvent);
        }
    }
}
