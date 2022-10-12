namespace TagS.Microservices.Client.DomainEventHandlers
{
    public class AddTagToReferrerDomainEventHandler : INotificationHandler<AddTagToReferrerDomainEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;
        private readonly ILogger<AddTagToReferrerDomainEventHandler> _logger;
        public AddTagToReferrerDomainEventHandler(IIntegrationEventService integrationEventService, ILogger<AddTagToReferrerDomainEventHandler> logger)
        {
            _integrationEventService = integrationEventService;
            _logger = logger;
        }
        public async Task Handle(AddTagToReferrerDomainEvent notification, CancellationToken cancellationToken)
        {
            var addTagIntegrationEvent = new AddReferrerToTagServerIntegrationEvent(notification.Referrer, notification.TagId);
            await _integrationEventService.SaveEventAsync(addTagIntegrationEvent);
        }
    }
}
