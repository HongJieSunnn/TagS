namespace TagS.Microservices.Client.Services
{
    public interface ITagIntegrationEventService : IIntegrationEventService
    {
        Task PublishEventAsync(Guid integrationEventId);
        Task PublishEventAsync(IEnumerable<Guid> integrationEventIds);
    }
}
