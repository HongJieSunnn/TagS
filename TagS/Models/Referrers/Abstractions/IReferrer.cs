using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.From;

namespace TagS.Models.Referrers.Abstractions
{
    public interface IReferrer<TReferrerId> 
        where TReferrerId : IEquatable<TReferrerId>
    {
        public Guid Guid { get; set; }//Each time call ToReferrer can not get same guid but ReferrerId is same generate by one tagable instance.
        TReferrerId ReferrerId { get; init; }//Unique 
        string TagableTypeJson { get;set; }//To deserialize to TagableType
        string FromType { get;set; }//To deserialize to FromType and to execute queries.
        string FromJson { get; set; }//Json Serializes by FromType(e:FromSQL).
        List<TagIdentityModel> Tags { get; set; }//Tag must be existed.Whaterver add referrer or update referrer,tag can not be added by referrer.
    }

    public class TagIdentityModel
    {
        public Guid TagGuid { get; set; }
        public string TagPreferredName { get; set; }

        public TagIdentityModel(Guid guid,string tagPreferredName)
        {
            TagGuid = guid;
            TagPreferredName = tagPreferredName;
        }
    }
}
