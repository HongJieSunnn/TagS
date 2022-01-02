using Innermost.IdempotentCommand;
using Innermost.IdempotentCommand.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Implementation.Commands.TagCommands;
using TagS.Implementation.Events.TagRaisedEvents;
using TagS.Infrastructure.Repositories.Abstractions;

namespace TagS.Implementation.CommandHandlers.TagCommandHandlers
{
    internal class RemoveTagCommandHandler<TPersisitence> : IRequestHandler<RemoveTagCommand<TPersisitence>, bool>
    {
        private readonly IMediator _mediator;
        private readonly ITagRepository<TPersisitence> _tagRepository;
        public RemoveTagCommandHandler(IMediator mediator,ITagRepository<TPersisitence> tagRepository)
        {
            _mediator = mediator;
            _tagRepository = tagRepository;
        }
        public async Task<bool> Handle(RemoveTagCommand<TPersisitence> request, CancellationToken cancellationToken)
        {
            await _tagRepository.DeleteAsync(request.Tag);

            var removedEvent=new TagRemovedEvent<TPersisitence>(request.Tag);

            var removedEventStatue = await _mediator.Send(removedEvent,cancellationToken);

            return removedEventStatue;
        }
    }

    internal class IdempotentRemoveTagCommandHandler<TPersisitence> : IdempotentCommandHandler<RemoveTagCommand<TPersisitence>, bool>
    {
        public IdempotentRemoveTagCommandHandler(IMediator mediator, ICommandRequestRepository commandRequestRepository) : base(mediator, commandRequestRepository)
        {
        }
    }
}
