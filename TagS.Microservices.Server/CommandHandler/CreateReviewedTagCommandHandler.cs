﻿using Innermost.IdempotentCommand.Infrastructure.Repositories;

namespace TagS.Microservices.Server.CommandHandler
{
    public class CreateReviewedTagCommandHandler : IRequestHandler<CreateReviewedTagCommand, bool>
    {
        private readonly ITagReviewedRepository _tagReviewedRepository;
        public CreateReviewedTagCommandHandler(ITagReviewedRepository tagReviewedRepository)
        {
            _tagReviewedRepository = tagReviewedRepository;
        }
        public async Task<bool> Handle(CreateReviewedTagCommand request, CancellationToken cancellationToken)
        {
            if (_tagReviewedRepository.ExistedPreferredName(request.PreferredTagName))
                return false;

            var reviewedTag = new TagReviewed(request.PreferredTagName, request.TagDetail, request.UserId, request.CreateTime!.Value, request.PreviousTagId, request.Ancestors, request.Synonyms);
            await _tagReviewedRepository.CreateReviewedTagAsync(reviewedTag);
            return true;
        }
    }

    public class IdempotentCreateReviewedTagCommandHandler : IdempotentCommandHandler<CreateReviewedTagCommand, bool>
    {
        public IdempotentCreateReviewedTagCommandHandler(IMediator mediator, ICommandRequestRepository commandRequestRepository) : base(mediator, commandRequestRepository)
        {
        }

        protected override bool CreateDefault()
        {
            return true;
        }
    }
}
