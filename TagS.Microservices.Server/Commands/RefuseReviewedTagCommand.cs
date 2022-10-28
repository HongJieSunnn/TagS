namespace TagS.Microservices.Server.Commands
{
    public class RefuseReviewedTagCommand : IRequest<bool>
    {
        public string ReviewedTagId { get; set; }
        public RefuseReviewedTagCommand(string reviewedTagId)
        {
            ReviewedTagId = reviewedTagId;
        }
    }
}
