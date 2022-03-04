namespace TagS.Microservices.Server.IntegrationEventHandler
{
    public class AddReferrerToTagServerIntegrationEventHandler : IIntegrationEventHandler<AddReferrerToTagServerIntegrationEvent>
    {
        
        private readonly ILogger<AddReferrerToTagServerIntegrationEventHandler> _logger;
        private readonly ITagWithReferrerRepository _tagWithReferrerRepository;
        public AddReferrerToTagServerIntegrationEventHandler(ILogger<AddReferrerToTagServerIntegrationEventHandler> logger, ITagWithReferrerRepository tagWithReferrerRepository)
        {
            _logger = logger;
            _tagWithReferrerRepository = tagWithReferrerRepository;

        }
        public async Task Handle(AddReferrerToTagServerIntegrationEvent @event)
        {
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Assembly.GetEntryAssembly().FullName, @event);
            await _tagWithReferrerRepository.AddReferrerToTagAsync(@event.TagId, @event.Referrer);
        }
    }
}
