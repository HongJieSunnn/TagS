using HongJieSun.TagS.Models.Tags;
using Innermost.IdempotentCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Implementation.Commands;
using TagS.Implementation.Commands.TagReferrerCommands;
using TagS.Models.Referrers.Generic;

namespace TagS.ApplicationInterface.Abstractions
{
    public abstract class Tagable<TReferrerId, TPersistence> : ITagable<TReferrerId, TPersistence>
        where TReferrerId : IEquatable<TReferrerId>
    {
        public Tagable()
        {
            
        }

        public virtual async Task<bool> AddTagAsync(Tag tag)
        {
            //var referrer = ToReferrer();
            //var command = new AddTagToReferrerCommand<TReferrerId, TPersistence>(tag, referrer);
            //var idempotentCommand = new IdempotentCommandLoader<AddTagToReferrerCommand<TReferrerId, TPersistence>, bool>(command, Guid.NewGuid());
            //return await _mediator.Send(idempotentCommand);
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveTagAsync(Tag tag)
        {
            //var referrer = ToReferrer();
            //var command = new RemoveTagForReferrerCommand<TReferrerId, TPersistence>(tag, referrer);
            //var idempotentCommand = new IdempotentCommandLoader<RemoveTagForReferrerCommand<TReferrerId, TPersistence>, bool>(command, Guid.NewGuid());
            //return await _mediator.Send(idempotentCommand);
            throw new NotImplementedException();
        }

        public abstract IReferrer<TReferrerId> ToReferrer();
    }
}
