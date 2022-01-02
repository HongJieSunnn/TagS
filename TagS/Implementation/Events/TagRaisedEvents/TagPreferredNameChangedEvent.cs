using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Implementation.Events.TagRaisedEvents
{
    internal class TagPreferredNameChangedEvent<TPerisistence>:IRequest<bool>
    {
        public string OriginTagPreferredName { get; set; }
        public string ChangedTagPreferredName { get; set; }
        public TagPreferredNameChangedEvent(string originName,string changedName)
        {
            OriginTagPreferredName = originName;
            ChangedTagPreferredName = changedName;
        }
    }
}
