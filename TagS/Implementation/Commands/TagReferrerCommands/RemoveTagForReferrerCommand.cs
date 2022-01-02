using HongJieSun.TagS.Models.Tags;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers.Generic;

namespace TagS.Implementation.Commands.TagReferrerCommands
{
    internal class RemoveTagForReferrerCommand<TReferrerId, TPersistence> : IRequest<bool>
        where TReferrerId : IEquatable<TReferrerId>
    {
        public Tag Tag { get; private set; }
        public IReferrer<TReferrerId> Referrer { get; private set; }
        public RemoveTagForReferrerCommand(Tag tag, IReferrer<TReferrerId> referrer)
        {
            Tag = tag;
            Referrer = referrer;
        }
    }
}
