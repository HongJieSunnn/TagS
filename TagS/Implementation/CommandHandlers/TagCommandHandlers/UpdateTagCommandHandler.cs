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
    internal class UpdateTagCommandHandler<TPersistence> : IRequestHandler<UpdateTagCommand<TPersistence>, bool>
    {
        private readonly IMediator _mediator;
        private readonly ITagRepository<TPersistence> _tagRepository;
        public UpdateTagCommandHandler(IMediator mediator,ITagRepository<TPersistence> tagRepository)
        {
            _mediator = mediator;
            _tagRepository = tagRepository;
        }

        public async Task<bool> Handle(UpdateTagCommand<TPersistence> request, CancellationToken cancellationToken)
        {
            await _tagRepository.UpdateAsync(request.UpdatedTag);

            var updatedEvent = new TagUpdatedEvent<TPersistence>(request.UpdatedTag);
            var updatedEventStatue = await _mediator.Send(updatedEvent, cancellationToken);

            return updatedEventStatue;
        }
    }

    internal class IdempotentUpdateTagCommandHandler<TPersistence> : IdempotentCommandHandler<UpdateTagCommand<TPersistence>, bool>
    {
        public IdempotentUpdateTagCommandHandler(IMediator mediator, ICommandRequestRepository commandRequestRepository) : base(mediator, commandRequestRepository)
        {
        }
    }
}
