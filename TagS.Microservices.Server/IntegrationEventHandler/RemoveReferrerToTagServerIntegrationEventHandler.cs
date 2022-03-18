namespace TagS.Microservices.Server.IntegrationEventHandler
{
    public class RemoveReferrerToTagServerIntegrationEventHandler : IIntegrationEventHandler<RemoveReferrerToTagServerIntegrationEvent>
    {
        private readonly ILogger<RemoveReferrerToTagServerIntegrationEventHandler> _logger;
        private readonly ITagWithReferrerRepository _tagWithReferrerRepository;
        public RemoveReferrerToTagServerIntegrationEventHandler(ILogger<RemoveReferrerToTagServerIntegrationEventHandler> logger, ITagWithReferrerRepository tagWithReferrerRepository)
        {
            _logger = logger;
            _tagWithReferrerRepository = tagWithReferrerRepository;
        }
        public async Task Handle(RemoveReferrerToTagServerIntegrationEvent @event)
        {
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Assembly.GetEntryAssembly().FullName, @event);
            await _tagWithReferrerRepository.RemoveReferrerToTagAsync(@event.TagId, @event.Referrer);
        }
    }
}
