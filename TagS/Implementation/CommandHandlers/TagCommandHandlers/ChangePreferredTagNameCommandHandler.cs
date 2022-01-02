using Innermost.IdempotentCommand;
using Innermost.IdempotentCommand.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Implementation.CommandHandlers.TagReferrerCommandHandlers;
using TagS.Implementation.Commands.TagCommands;
using TagS.Implementation.Events.TagRaisedEvents;
using TagS.Infrastructure.Repositories.Abstractions;

namespace TagS.Implementation.CommandHandlers.TagCommandHandlers
{
    internal class ChangePreferredTagNameCommandHandler<TPersistence> : IRequestHandler<ChangePreferredTagNameCommand<TPersistence>, bool>
    {
        private readonly IMediator _mediator;
        private readonly ITagRepository<TPersistence> _tagRepository;
        public ChangePreferredTagNameCommandHandler(IMediator mediator,ITagRepository<TPersistence> tagRepository)
        {
            _mediator = mediator;
            _tagRepository = tagRepository;
        }

        public async Task<bool> Handle(ChangePreferredTagNameCommand<TPersistence> request, CancellationToken cancellationToken)
        {
            if (request.PreferredTagNameToChange == request.Tag.PreferredTagName)//TODO if AI judge that PreferredTagNameToChange is not symnonym of PreferredTagName that return false.
                return false;

            if(request.Tag.Synonyms.Contains(request.PreferredTagNameToChange))
            {
                request.Tag.Synonyms.Remove(request.PreferredTagNameToChange);
                request.Tag.Synonyms.Add(request.Tag.PreferredTagName);
            }

            var tagChangedEvent = new TagPreferredNameChangedEvent<TPersistence>(request.Tag.PreferredTagName, request.PreferredTagNameToChange);
            var tagChangedEventTask = _mediator.Send(tagChangedEvent,cancellationToken);

            request.Tag.PreferredTagName = request.PreferredTagNameToChange;
            await _tagRepository.UpdateAsync(request.Tag);

            if(await tagChangedEventTask == false)
            {
                //we can get affected referrers by TagPreferredNameChangedEventCommand's return value and then to cancel the change.
                throw new InvalidOperationException("Change preferred name of referrers failed.However,the origin tagPreferredName is become a symnonym of new preferred name.");
            }

            return true;
        }
    }

    internal class IdempotentChangePreferredTagNameCommandHandler<TPersistence> : IdempotentCommandHandler<ChangePreferredTagNameCommand<TPersistence>, bool>
    {
        public IdempotentChangePreferredTagNameCommandHandler(IMediator mediator, ICommandRequestRepository commandRequestRepository) : base(mediator, commandRequestRepository)
        {
        }
    }
}
