namespace TagS.Microservices.Server.Models
{
    public class TagWithReferrer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string PreferredTagName { get; set; }
        public string TagDetail { get; set; }
        public List<string> Synonyms { get; set; }
        public List<IReferrer> Referrers { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? UpdateTime { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? DeleteTime { get; set; }
        public TagWithReferrer(string id, string preferredTagName, string tagDetail, List<string> synonyms, List<IReferrer>? referrers, DateTime createTime)
        {
            Id = id;
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            Synonyms = synonyms;
            Referrers = referrers ?? new List<IReferrer>();
            CreateTime = createTime;
        }
    }
}
