using HongJieSun.TagS.Models.Tags;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Implementation.Events.TagRaisedEvents
{
    internal class TagUpdatedEvent<TPersistence>:IRequest<bool>
    {
        public Tag UpdatedTag { get; set; }
        public TagUpdatedEvent(Tag updatedTag)
        {
            UpdatedTag = updatedTag;
        }
    }
}
