using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Models.Referrers.Generic
{
    public interface IReferrer<TReferrerId> 
        where TReferrerId : IEquatable<TReferrerId>
    {
        ObjectId ObjectId { get; init; }
        TReferrerId ReferrerId { get; init; }
        string From { get; set; }
        /// <summary>
        /// Store tags' prefered name.
        /// </summary>
        IEnumerable<string> Tags { get; set; }
    }
}
