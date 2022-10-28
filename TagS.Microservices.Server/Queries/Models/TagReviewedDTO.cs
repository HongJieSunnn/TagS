namespace TagS.Microservices.Server.Queries.Models
{
    public class TagReviewedDTO
    {
        public string Id { get; init; }
        public string PreferredTagName { get; init; }
        public string TagDetail { get; init; }
        public string? PreviousTagId { get; init; }
        public List<string>? Ancestors { get; init; }
        public string UserId { get; init; }
        public DateTime CreateTime { get; init; }
        public TagReviewedDTO(string id, string preferredTagName, string tagDetail, string? previousTagId, List<string>? ancestors, string userId, DateTime creteTime)
        {
            Id = id;
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            PreviousTagId = previousTagId;
            Ancestors = ancestors;
            UserId = userId;
            CreateTime = creteTime;
        }
    }
}
