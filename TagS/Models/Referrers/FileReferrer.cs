using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers.Abstractions;

namespace TagS.Models.Referrers
{

    public class FileReferrer : IReferrer<string>//TODO
    {
        public Guid Guid { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ReferrerId { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
        public string TagableTypeJson { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FromType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FromJson { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<TagIdentityModel> Tags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
