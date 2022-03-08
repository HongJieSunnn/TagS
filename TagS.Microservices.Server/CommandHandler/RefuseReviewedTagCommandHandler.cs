namespace TagS.Microservices.Server.CommandHandler
{
    internal class RefuseReviewedTagCommandHandler : IRequestHandler<RefuseReviewedTagCommand, bool>
    {
        private readonly ITagReviewedRepository _tagReviewedRepository;
        public RefuseReviewedTagCommandHandler(ITagReviewedRepository tagReviewedRepository)
        {
            _tagReviewedRepository = tagReviewedRepository;

        }
        public async Task<bool> Handle(RefuseReviewedTagCommand request, CancellationToken cancellationToken)
        {
            var result = await _tagReviewedRepository.RefuseReviewedTagAsync(request.ReviewedTagId);
            return result;
        }
    }
}
