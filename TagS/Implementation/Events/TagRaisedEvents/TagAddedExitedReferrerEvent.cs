using HongJieSun.TagS.Models.Tags;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Implementation.Events.TagRaisedEvents
{
    internal class TagAddedExitedReferrerEvent<TPersistence> : IRequest<bool>
    {
        public Tag Tag { get; private set; }
        public IEnumerable<ObjectId> Referrers { get; private set; }

        public TagAddedExitedReferrerEvent(Tag tag, ObjectId referrer)
        {
            Tag = tag;
            Referrers = new List<ObjectId>() { referrer };
        }

        public TagAddedExitedReferrerEvent(Tag tag, IEnumerable<ObjectId> referrer)
        {
            Tag = tag;
            Referrers = referrer;
        }
    }
}
