using Innermost.IdempotentCommand;
using Innermost.IdempotentCommand.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Implementation.Commands.TagCommands;
using TagS.Implementation.Commands.TagReferrerCommands;
using TagS.Implementation.Events.TagRaisedEvents;
using TagS.Infrastructure.Repositories.Abstractions;

namespace TagS.Implementation.CommandHandlers.TagCommandHandlers
{
    internal class AddExitedReferrerToTagCommandHandler<TPersistence> : IRequestHandler<ReferrerAddedTagEvent<TPersistence>, bool>
    {
        private readonly IMediator _mediator;
        private readonly ITagReferrerRepository<TPersistence> _tagReferrerRepository;
        public AddExitedReferrerToTagCommandHandler(IMediator mediator,ITagReferrerRepository<TPersistence> tagReferrerRepository)
        {
            _mediator = mediator;
            _tagReferrerRepository= tagReferrerRepository;
        }
        public async Task<bool> Handle(ReferrerAddedTagEvent<TPersistence> request, CancellationToken cancellationToken)
        {
            if (!request.Referrers.Any())
                throw new ArgumentException("Referrers must not be empty.");
            if (!_tagReferrerRepository.Existed(request.Referrers))
                throw new ArgumentException("At least one Referrer passed is not existed.");

            request.Tag.Referrers.UnionWith(request.Referrers);
            var updateTagCommand = new UpdateTagCommand<TPersistence>(request.Tag);
            var updateTagCommandStatue=await _mediator.Send(updateTagCommand);

            if (updateTagCommandStatue==true)
            {
                var tagAddedExitedReferrerEvent = new TagAddedExitedReferrerEvent<TPersistence>(request.Tag, request.Referrers);

                return await _mediator.Send(tagAddedExitedReferrerEvent, cancellationToken);//To test if add all failed whether should to remove tag for referrers who added successfully.
            }

            return false;
        }
    }

    internal class IdempotentAddExitedReferrerToTagCommandHandler<TPersistence> : IdempotentCommandHandler<ReferrerAddedTagEvent<TPersistence>, bool>
    {
        public IdempotentAddExitedReferrerToTagCommandHandler(IMediator mediator, ICommandRequestRepository commandRequestRepository) : base(mediator, commandRequestRepository)
        {
        }
    }
}
