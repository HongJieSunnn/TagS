namespace TagS.Microservices.Client.IntegrationEvents
{
    public record AddReferrerToTagServerIntegrationEvent : IntegrationEvent
    {
        public IReferrer Referrer { get;set; }
        public string TagId { get; set; }
        public AddReferrerToTagServerIntegrationEvent(IReferrer referrer,string tagId)
        {
            Referrer = referrer;
            TagId = tagId;
        }
    }
}
