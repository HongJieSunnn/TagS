namespace TagS.Microservices.Server.Commands
{
    public class CreateReviewedTagCommand:IRequest<bool>
    {
        public string PreferredTagName { get; set; }
        public string TagDetail { get; set; }
        public string? PreviousTagId { get; set; }
        public string? FirstLevelTagId { get; set; }
        public string UserId { get; set; }
        public DateTime CreateTime { get; set; }
        public CreateReviewedTagCommand(string preferredTagName,string tagDetail,string userId,DateTime createTime, string? previousTagId=null,string? firstLevelTagId=null)
        {
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            PreviousTagId = previousTagId;
            FirstLevelTagId = firstLevelTagId;
            UserId = userId;
            CreateTime = createTime==default(DateTime)?DateTime.Now:createTime;
        }
    }
}
