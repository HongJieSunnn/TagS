using HongJieSun.TagS.Models.Tags;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Implementation.Events.TagReferrerRaisedEvents
{
    internal class ReferrerRemovedTagEvent<TPersistence>:IRequest<bool>
    {
        public Tag RemovedTag { get; set; }
        public ObjectId ReferrerObjectId { get; set; }
        public ReferrerRemovedTagEvent(Tag removedTag,ObjectId referrerObjectId)
        {
            RemovedTag = removedTag;
            ReferrerObjectId = referrerObjectId;
        }
    }
}
