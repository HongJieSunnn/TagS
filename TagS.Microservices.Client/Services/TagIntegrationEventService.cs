using IntegrationEventRecordMongoDB.Services;
using System.Reflection;

namespace TagS.Microservices.Client.Services
{
    public class TagIntegrationEventService : ITagIntegrationEventService
    {
        private readonly IAsyncEventBus _eventBus;
        private readonly ILogger<TagIntegrationEventService> _logger;
        private readonly IIntegrationEventRecordMongoDBService _integrationEventRecordMongoDBService;
        public TagIntegrationEventService(IAsyncEventBus eventBus,ILogger<TagIntegrationEventService> logger,IIntegrationEventRecordMongoDBService integrationEventRecordMongoDBService)
        {
            _eventBus = eventBus;
            _logger = logger;
            _integrationEventRecordMongoDBService = integrationEventRecordMongoDBService;

        }
        public async Task AddAndSaveEventAsync(IntegrationEvent integrationEvent)
        {
            _logger.LogInformation("----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", integrationEvent.Id, integrationEvent);
            await _integrationEventRecordMongoDBService.SaveEventAsync(integrationEvent);
        }

        public async Task PublishEventAsync(Guid integrationEventId)
        {
            await PublishEventAsync(new List<Guid>(1) { integrationEventId });
        }

        public async Task PublishEventAsync(IEnumerable<Guid> integrationEventIds)
        {
            var records=await _integrationEventRecordMongoDBService.GetIntegrationEventRecords(integrationEventIds);
            foreach (var record in records)
            {
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", record.EventId, Assembly.GetEntryAssembly()?.FullName,record.EventContent);

                try
                {
                    await _integrationEventRecordMongoDBService.MarkEventAsInProcessAsync(record.EventId);
                    await _eventBus.Publish(record.EventContent);
                    await _integrationEventRecordMongoDBService.MarkEventAsPublishedAsync(record.EventId);
                }
                catch (Exception ex)
                {
                    //TODO 加上发送邮件？
                    _logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId} from {AppName}", record.EventId, Assembly.GetEntryAssembly()?.FullName);
                    throw;
                }
            }
        }
    }
}
