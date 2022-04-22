namespace TagS.Microservices.Server.Queries.Models
{
    public class TagDTO
    {
        public string Id { get; init; }
        public string PreferredTagName { get; init; }
        public string TagDetail { get; init; }
        public string? PreviousTagId { get; init; }
        public List<string>? Ancestors { get; init; }
        public List<string> Synonyms { get; init; }
        public List<string> RelatedTagIds { get;init; }
        public DateTime CreateTime { get; init; }
        public TagDTO(string id,string preferredTagName,string tagDetail,string? previousTagId, List<string>? ancestors,List<string>? synonyms,List<string>? relatedTagIds,DateTime createTime)
        {
            Id=id;
            PreferredTagName=preferredTagName;
            TagDetail=tagDetail;
            PreviousTagId=previousTagId;
            Ancestors=ancestors;
            Synonyms=synonyms??new List<string>();
            RelatedTagIds=relatedTagIds??new List<string>();
            CreateTime=createTime;
        }
    }
}
