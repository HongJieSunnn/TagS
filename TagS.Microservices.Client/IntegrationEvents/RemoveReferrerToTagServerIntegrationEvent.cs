﻿namespace TagS.Microservices.Client.IntegrationEvents
{
    public record RemoveReferrerToTagServerIntegrationEvent : IntegrationEvent
    {
        public IReferrer Referrer { get; set; }
        public string TagId { get; set; }
        public RemoveReferrerToTagServerIntegrationEvent(IReferrer referrer, string tagId)
        {
            Referrer = referrer;
            TagId = tagId;
        }
    }
}
