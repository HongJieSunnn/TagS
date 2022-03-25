namespace TagS.Microservices.Server.Commands
{
    public class CreateReviewedTagCommand:IRequest<bool>
    {
        public string PreferredTagName { get; set; }
        public string TagDetail { get; set; }
        public string? PreviousTagId { get; set; }
        public List<string>? Ancestors { get; set; }
        public string UserId { get; set; }
        public DateTime CreateTime { get; set; }
        public CreateReviewedTagCommand(string preferredTagName,string tagDetail,string userId,DateTime createTime, string? previousTagId=null, List<string>? ancestors=null)
        {
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            PreviousTagId = previousTagId;
            Ancestors = ancestors;
            UserId = userId;
            CreateTime = createTime==default(DateTime)?DateTime.Now:createTime;
        }
    }
}
