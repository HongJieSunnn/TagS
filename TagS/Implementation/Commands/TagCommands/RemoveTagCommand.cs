using HongJieSun.TagS.Models.Tags;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers.Generic;

namespace TagS.Implementation.Commands.TagCommands
{
    internal class RemoveTagCommand<TPersistence> : IRequest<bool>
    {
        public Tag Tag { get; private set; }
        public RemoveTagCommand(Tag tag)
        {
            Tag = tag;
        }
    }
}
