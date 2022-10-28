using Innermost.IdempotentCommand.Infrastructure.Repositories;

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
            var updateResult = await _tagReviewedRepository.PassReviewedTagAsync(request.ReviewedTagId);
            if (updateResult.result)
            {
                await _tagReviewedRepository.UnitOfWork.SaveEntitiesAsync(updateResult.entity!, cancellationToken);
            }
            return updateResult.result;
        }
    }

    public class IdempotentPassReviewedTagCommandHandler : IdempotentCommandHandler<PassReviewedTagCommand, bool>
    {
        public IdempotentPassReviewedTagCommandHandler(IMediator mediator, ICommandRequestRepository commandRequestRepository) : base(mediator, commandRequestRepository)
        {
        }

        protected override bool CreateDefault()
        {
            return true;
        }
    }
}
