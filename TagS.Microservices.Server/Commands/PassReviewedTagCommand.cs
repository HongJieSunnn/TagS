namespace TagS.Microservices.Server.Commands
{
    public class PassReviewedTagCommand:IRequest<bool>
    {
        public string ReviewedTagId { get;set; }
        public PassReviewedTagCommand(string reviewedTagId)
        {
            ReviewedTagId = reviewedTagId;
        }
    }
}
