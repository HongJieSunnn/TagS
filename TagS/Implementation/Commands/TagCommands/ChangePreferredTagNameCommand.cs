using HongJieSun.TagS.Models.Tags;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Implementation.Commands.TagCommands
{
    internal class ChangePreferredTagNameCommand<TPersistence>:IRequest<bool>
    {
        public Tag Tag { get; set; }
        public string PreferredTagNameToChange { get;set; }

        public ChangePreferredTagNameCommand(Tag tag,string preferredTagName)
        {
            Tag = tag;
            PreferredTagNameToChange = preferredTagName;
        }
    }
}
