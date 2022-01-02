using HongJieSun.TagS.Models.Tags;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Implementation.Commands.TagCommands
{
    internal class UpdateTagCommand<TPersistence>:IRequest<bool>
    {
        public Tag UpdatedTag { get; set; }
        public UpdateTagCommand(Tag updatedTag)
        {
            UpdatedTag=updatedTag;
        }
    }
}
