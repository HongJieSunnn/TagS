using Innermost.IdempotentCommand.Infrastructure.Repositories;

namespace TagS.Microservices.Server.CommandHandler
{
    public class RefuseReviewedTagCommandHandler : IRequestHandler<RefuseReviewedTagCommand, bool>
    {
        private readonly ITagReviewedRepository _tagReviewedRepository;
        public RefuseReviewedTagCommandHandler(ITagReviewedRepository tagReviewedRepository)
        {
            _tagReviewedRepository = tagReviewedRepository;

        }
        public async Task<bool> Handle(RefuseReviewedTagCommand request, CancellationToken cancellationToken)
        {
            var updateResult = await _tagReviewedRepository.RefuseReviewedTagAsync(request.ReviewedTagId);
            return updateResult.result;
        }
    }

    public class IdempotentRefuseReviewedTagCommandHandler : IdempotentCommandHandler<RefuseReviewedTagCommand, bool>
    {
        public IdempotentRefuseReviewedTagCommandHandler(IMediator mediator, ICommandRequestRepository commandRequestRepository) : base(mediator, commandRequestRepository)
        {
        }

        protected override bool CreateDefault()
        {
            return true;
        }
    }
}
