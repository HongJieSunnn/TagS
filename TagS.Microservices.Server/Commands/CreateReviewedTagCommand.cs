namespace TagS.Microservices.Server.Commands
{
    public class CreateReviewedTagCommand:IRequest<bool>
    {
        public string PreferredTagName { get; private set; }
        public string TagDetail { get; private set; }
        public string? PreviousTagId { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime CreateTime { get; private set; }
        public CreateReviewedTagCommand(string preferredTagName,string tagDetail,Guid userId,DateTime createTime, string? previousTagId=null)
        {
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            PreviousTagId = previousTagId;
            UserId = userId;
            CreateTime = createTime;
        }
    }
}
