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
            await _integrationEventService.AddAndSaveEventAsync(addTagIntegrationEvent);

            //It seems like we should not SaveEntities in this handler.CommandHandler SaveEntities and send domainEvent to this handler.
            //if(_integrationEventService.GetType()!=typeof(ITagIntegrationEventService))
            //{
            //    if(_repository is not null)
            //    {
            //        //Save the tag add to aggregate.IntegrationEvent will be published in mediatR behavior.
            //        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            //        return;
            //    }
            //    throw new NullReferenceException($"While use repository to SaveEntity,the Repository {nameof(_repository)} can not be null.");
            //}
            if(_integrationEventService.GetType() == typeof(ITagIntegrationEventService))
                await (_integrationEventService as ITagIntegrationEventService)!.PublishEventAsync(addTagIntegrationEvent.Id);
        }
    }
}
