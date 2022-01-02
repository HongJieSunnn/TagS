using Innermost.IdempotentCommand;
using Innermost.IdempotentCommand.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Implementation.Commands;
using TagS.Implementation.Commands.TagReferrerCommands;
using TagS.Infrastructure.Repositories.Abstractions;

namespace TagS.Implementation.CommandHandlers.TagReferrerCommandHandlers
{
    internal class AddTagReferrerCommandHandler<TReferrerId, TPersistence> : IRequestHandler<AddTagReferrerCommand<TReferrerId, TPersistence>, bool>
        where TReferrerId : IEquatable<TReferrerId>
    {
        private readonly ITagReferrerRepository<TPersistence> _referrerRepository;
        public AddTagReferrerCommandHandler(ITagReferrerRepository<TPersistence> referrerRepository)
        {
            _referrerRepository = referrerRepository;
        }
        public async Task<bool> Handle(AddTagReferrerCommand<TReferrerId, TPersistence> request, CancellationToken cancellationToken)
        {
            if (_referrerRepository.Existed(request.Referrer))
                return false;

            await _referrerRepository.AddAsync(request.Referrer);
            return true;
        }
    }

    internal class IdempotentAddTagReferrerCommandHandler<TReferrerId, TPersistence> : IdempotentCommandHandler<AddTagReferrerCommand<TReferrerId, TPersistence>, bool>
        where TReferrerId : IEquatable<TReferrerId>
    {
        public IdempotentAddTagReferrerCommandHandler(IMediator mediator, ICommandRequestRepository commandRequestRepository) : base(mediator, commandRequestRepository)
        {
        }
    }
}
