using Innermost.IdempotentCommand;
using Innermost.IdempotentCommand.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Implementation.Commands.TagReferrerCommands;
using TagS.Implementation.Events.TagReferrerRaisedEvents;
using TagS.Infrastructure.Repositories.Abstractions;

namespace TagS.Implementation.CommandHandlers.TagReferrerCommandHandlers
{
    internal class RemoveTagForReferrerCommandHandler<TReferrerId, TPersistence> : IRequestHandler<RemoveTagForReferrerCommand<TReferrerId, TPersistence>, bool>
        where TReferrerId : IEquatable<TReferrerId>
    {
        private readonly IMediator _mediator;
        private readonly ITagReferrerRepository<TPersistence> _tagReferrerRepository;
        public RemoveTagForReferrerCommandHandler(IMediator mediator,ITagReferrerRepository<TPersistence> tagReferrerRepository)
        {
            _mediator = mediator;
            _tagReferrerRepository = tagReferrerRepository;
        }
        public async Task<bool> Handle(RemoveTagForReferrerCommand<TReferrerId, TPersistence> request, CancellationToken cancellationToken)
        {
            await _tagReferrerRepository.RemoveTagAsync(request.Referrer, request.Tag);

            var referrerRemovedTagEvent=new ReferrerRemovedTagEvent<TPersistence>(request.Tag, request.Referrer.ObjectId);

            var eventStatue=await _mediator.Send(referrerRemovedTagEvent, cancellationToken);

            return eventStatue;
        }
    }

    internal class IdempotentRemoveTagForReferrerCommandHandler<TReferrerId, TPersistence> : IdempotentCommandHandler<RemoveTagForReferrerCommand<TReferrerId, TPersistence>, bool>
        where TReferrerId : IEquatable<TReferrerId>
    {
        public IdempotentRemoveTagForReferrerCommandHandler(IMediator mediator, ICommandRequestRepository commandRequestRepository) : base(mediator, commandRequestRepository)
        {
        }
    }
}
