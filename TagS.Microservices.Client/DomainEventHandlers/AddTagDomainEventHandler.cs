using TagS.Microservices.Client.Services;

namespace TagS.Microservices.Client.DomainEventHandlers
{
    public class AddTagDomainEventHandler : INotificationHandler<AddTagDomainEvent>
    {
        private readonly IRepository<IAggregateRoot>? _repository;
        private readonly IIntegrationEventService _integrationEventService;
        private readonly ILogger<AddTagDomainEventHandler> _logger;
        public AddTagDomainEventHandler(IIntegrationEventService integrationEventService, ILogger<AddTagDomainEventHandler> logger, IRepository<IAggregateRoot>? repository=null)
        {
            _repository = repository;
            _integrationEventService = integrationEventService;
            _logger = logger;
        }
        public async Task Handle(AddTagDomainEvent notification, CancellationToken cancellationToken)
        {
            var addTagIntegrationEvent = new AddReferrerToTagServerIntegrationEvent(notification.Referrer, notification.TagId);
            await _integrationEventService.AddAndSaveEventAsync(addTagIntegrationEvent);

            if(_integrationEventService.GetType()!=typeof(ITagIntegrationEventService))
            {
                if(_repository is not null)
                {
                    //Save the tag add to aggregate.IntegrationEvent will be published in mediatR behavior.
                    await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                    return;
                }
            }

            //TODO
            //Publish event by ITagIntegrationEventService
            //mongodb do not use SaveChanges to Save Changes.
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
