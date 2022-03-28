namespace TagS.Microservices.Client.Models
{
    public interface IReferrer
    {
        string? Id { get; set; }//We store Referrers in MongoDB so the Id type can just be string?.
        /// <summary>
        /// We use referrerName to classify.
        /// For example,We want get LifeRecord with tag Emotion:Happy.We get all referrer with referrerName LifeRecord.
        /// </summary>
        string ReferrerName { get; }
        /// <summary>
        /// ReferrerId's type may be not only string.
        /// But all of them can convert to string.
        /// </summary>
        string ReferrerId { get; set; }
    }
}
