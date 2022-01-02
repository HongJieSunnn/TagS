using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers.Generic;

namespace TagS.Models.Referrers
{

    public class FileReferrer:IReferrer<string>//TODO
    {
        public ObjectId ObjectId { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
        public string ReferrerId { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
        public string From { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<string> Tags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
