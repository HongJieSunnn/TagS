namespace TagS.Microservices.Server.Queries.Models
{
    public class TagReviewedDTO
    {
        public string Id { get; set; }
        public string PreferredTagName { get; set; }
        public string TagDetail { get; set; }
        public string? PreviousTagId { get; set; }
        public List<string>? Ancestors { get; set; }
        public string UserId { get; set; }
        public DateTime CreateTime { get; set; }
        public TagReviewedDTO(string id,string preferredTagName,string tagDetail,string? previousTagId,List<string>? ancestors,string userId,DateTime creteTime)
        {
            Id=id;
            PreferredTagName=preferredTagName;
            TagDetail=tagDetail;
            PreviousTagId=previousTagId;
            Ancestors = ancestors;
            UserId=userId;
            CreateTime= creteTime;
        }
    }
}
