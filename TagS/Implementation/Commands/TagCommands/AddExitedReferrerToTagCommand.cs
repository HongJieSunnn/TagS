using HongJieSun.TagS.Models.Tags;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Implementation.Commands.TagCommands
{
    internal class ReferrerAddedTagEvent<TPersistence> :IRequest<bool>
    {
        public Tag Tag { get; set; }
        public IEnumerable<ObjectId> Referrers { get; set; }
        public ReferrerAddedTagEvent(Tag tag, ObjectId referrer)
        {
            Tag = tag;
            Referrers = new List<ObjectId>() { referrer };
        }
        public ReferrerAddedTagEvent(Tag tag, IEnumerable<ObjectId> referrers)
        {
            Tag = tag;
            Referrers = referrers;
        }
    }
}
