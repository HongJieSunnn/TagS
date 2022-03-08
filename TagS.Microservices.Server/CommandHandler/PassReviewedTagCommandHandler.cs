namespace TagS.Microservices.Server.CommandHandler
{
    public class PassReviewedTagCommandHandler : IRequestHandler<PassReviewedTagCommand, bool>
    {
        private readonly ITagReviewedRepository _tagReviewedRepository;
        public PassReviewedTagCommandHandler(ITagReviewedRepository tagReviewedRepository)
        {
            _tagReviewedRepository = tagReviewedRepository;

        }
        public async Task<bool> Handle(PassReviewedTagCommand request, CancellationToken cancellationToken)
        {
            var result = await _tagReviewedRepository.PassReviewedTagAsync(request.ReviewedTagId);
            if (result)
            {
                var tag = await _tagReviewedRepository.GetTagReviewedAsync(request.ReviewedTagId);
                await _tagReviewedRepository.UnitOfWork.SaveEntitiesAsync(tag,cancellationToken);
            }
            return result;
        }
    }
}
