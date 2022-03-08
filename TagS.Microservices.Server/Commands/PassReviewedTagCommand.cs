namespace TagS.Microservices.Server.Commands
{
    public class PassReviewedTagCommand:IRequest<bool>
    {
        public string ReviewedTagId { get;private set; }
        public PassReviewedTagCommand(string reviewedTagId)
        {
            ReviewedTagId = reviewedTagId;
        }
    }
}
