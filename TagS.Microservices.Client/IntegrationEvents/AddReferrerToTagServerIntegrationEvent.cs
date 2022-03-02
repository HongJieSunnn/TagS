using EventBusCommon;

namespace TagS.Microservices.Client.IntegrationEvents
{
    public record AddReferrerToTagServerIntegrationEvent : IntegrationEvent
    {
        public IReferrer Referrer { get;private set; }
        public string TagId { get; set; }
        public AddReferrerToTagServerIntegrationEvent(IReferrer referrer,string tagId)
        {
            Referrer = referrer;
            TagId = tagId;
        }
    }
}
