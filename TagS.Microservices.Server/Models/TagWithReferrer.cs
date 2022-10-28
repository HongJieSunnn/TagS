namespace TagS.Microservices.Server.Models
{
    public class TagWithReferrer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string PreferredTagName { get; set; }
        public string TagDetail { get; set; }

        [BsonRequired]
        public string? PreviousTagId { get; private set; }

        [BsonRequired]
        public List<string>? Ancestors { get; private set; }
        public List<string> Synonyms { get; set; }

        /// <summary>
        /// While referrer in referrers updates,we replace it.
        /// </summary>
        public List<IReferrer> Referrers { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? UpdateTime { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? DeleteTime { get; set; }
        public TagWithReferrer(string id, string preferredTagName, string tagDetail, List<string> synonyms, List<IReferrer>? referrers, DateTime createTime, string? previousTagId = null, List<string>? ancestors = null)
        {
            Id = id;
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            PreviousTagId = previousTagId;
            Ancestors = ancestors;
            Synonyms = synonyms;
            Referrers = referrers ?? new List<IReferrer>();
            CreateTime = createTime;
        }
    }
}
