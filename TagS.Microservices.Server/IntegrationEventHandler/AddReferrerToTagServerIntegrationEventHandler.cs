using MongoDB.Driver.GeoJsonObjectModel;

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
            //We should create a new AddReferrerToTagServerIntegrationEventHandler and Subscript that to do the extra operations.
            //However,for convenience,I do here.
            if (@event.Referrer.ReferrerName == "LifeRecord")
            {
                (@event.Referrer as dynamic).BaiduPOI = 
                    ((@event.Referrer as dynamic).Longitude is null || (@event.Referrer as dynamic).Latitude is null) ?
                    null 
                    : 
                    new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates((@event.Referrer as dynamic).Longitude, (@event.Referrer as dynamic).Latitude));
            }

            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Assembly.GetEntryAssembly().FullName, @event);
            await _tagWithReferrerRepository.AddReferrerToTagAsync(@event.TagId, @event.Referrer);
        }
    }
}
