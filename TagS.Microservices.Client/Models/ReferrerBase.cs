namespace TagS.Microservices.Client.Models
{
    public abstract class ReferrerBase : IReferrer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public virtual string ReferrerName => this.GetType().Name.Replace("Referrer","");
        public virtual string ReferrerId { get; set; }
        public ReferrerBase()
        {

        }
        public ReferrerBase(dynamic referrerId)
        {
            Id=ObjectId.GenerateNewId().ToString();
            ReferrerId = referrerId.ToString();
        }

        public override bool Equals(object? obj)
        {
            if(obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;

            var other = (ReferrerBase)obj;
            return this.ReferrerName.Equals(other.ReferrerName) && this.ReferrerId.Equals(other.ReferrerId);
        }

        public override int GetHashCode()
        {
            return ReferrerName.GetHashCode()+ReferrerId.GetHashCode();
        }
    }
}
