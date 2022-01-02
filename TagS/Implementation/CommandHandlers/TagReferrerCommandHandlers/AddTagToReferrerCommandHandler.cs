using Innermost.IdempotentCommand;
using Innermost.IdempotentCommand.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Implementation.Commands;
using TagS.Implementation.Commands.TagCommands;
using TagS.Implementation.Commands.TagReferrerCommands;
using TagS.Infrastructure.Repositories.Abstractions;

namespace TagS.Implementation.CommandHandlers.TagReferrerCommandHandlers
{
    internal class AddTagToReferrerCommandHandler<TReferrerId, TPersistence> : IRequestHandler<AddTagToReferrerCommand<TReferrerId,TPersistence>, bool>
        where TReferrerId : IEquatable<TReferrerId>
    {
        private readonly IMediator _mediator;
        private readonly ITagReferrerRepository<TPersistence> _referrerRepository;
        public AddTagToReferrerCommandHandler(IMediator mediator,ITagReferrerRepository<TPersistence> tagReferrerRepository)
        {
            _mediator = mediator;
            _referrerRepository = tagReferrerRepository;
        }
        public async Task<bool> Handle(AddTagToReferrerCommand<TReferrerId, TPersistence> request, CancellationToken cancellationToken)
        {
            if(!_referrerRepository.Existed(request.Referrer))
            {
                var addReferrerStatue= await _mediator.Send(new AddTagReferrerCommand<TReferrerId,TPersistence>(request.Referrer),cancellationToken);
                if (!addReferrerStatue)
                    throw new ArgumentException($"Referrer(ObjectId:{request.Referrer.ObjectId}) is already existed but was be judged as not existed while handle AddTagToReferrerCommand.");
            }
            
            var addTagStatueTask=_mediator.Send(new ReferrerAddedTagEvent<TPersistence>(request.Tag,request.Referrer.ObjectId), cancellationToken);

            var referenceCount = await _referrerRepository.AddTagAsync(request.Referrer, request.Tag);

            if (referenceCount == 0)
                return false;

            if(!await addTagStatueTask)
            {
                await _referrerRepository.RemoveTagAsync(request.Referrer,request.Tag);
                throw new ArgumentException($"Add tag(PreferredTagName:{request.Tag.PreferredTagName} failed.)");
            }

            return true;
        }
    }

    internal class IdempotentAddTagForReferrerCommandHandler<TReferrerId, TPersistence> : IdempotentCommandHandler<AddTagToReferrerCommand<TReferrerId, TPersistence>, bool>
        where TReferrerId : IEquatable<TReferrerId>
    {
        public IdempotentAddTagForReferrerCommandHandler(IMediator mediator, ICommandRequestRepository commandRequestRepository) : base(mediator, commandRequestRepository)
        {
        }
    }
}
