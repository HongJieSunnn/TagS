namespace TagS.Microservices.Server.Queries.Models
{
    public class TagDTO
    {
        public string Id { get; set; }
        public string PreferredTagName { get; set; }
        public string TagDetail { get; set; }
        public string? PreviousTagId { get; set; }
        public List<string>? Ancestors { get; set; }
        public List<string> Synonyms { get; set; }
        public List<string> RelatedTagIds { get;set; }
        public DateTime CreateTime { get; set; }
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
