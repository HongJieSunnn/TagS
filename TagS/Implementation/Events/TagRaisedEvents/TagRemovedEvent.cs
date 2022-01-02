using HongJieSun.TagS.Models.Tags;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Implementation.Events.TagRaisedEvents
{
    internal class TagRemovedEvent<TPersistence>:IRequest<bool>
    {
        public Tag RemovedTag { get; set; }
        public TagRemovedEvent(Tag removedTag)
        {
            RemovedTag = removedTag;
        }
    }
}
