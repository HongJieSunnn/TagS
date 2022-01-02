using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers.Generic;

namespace TagS.Implementation.Commands.TagReferrerCommands
{
    internal class AddTagReferrerCommand<TReferrerId, TPersistence> : IRequest<bool>
        where TReferrerId : IEquatable<TReferrerId>
    {
        public IReferrer<TReferrerId> Referrer { get; private set; }
        public AddTagReferrerCommand(IReferrer<TReferrerId> referrer)
        {
            Referrer = referrer;
        }
    }
}
