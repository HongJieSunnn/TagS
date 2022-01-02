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
    internal class AddTagCommand<TPersistence>:IRequest<bool>
    {
        public Tag Tag { get;private set; }
        public AddTagCommand(Tag tag)
        {
            Tag = tag;
        }
    }
}
