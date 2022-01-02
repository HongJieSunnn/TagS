using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Models.Referrers.Abstractions
{
    internal interface IReferrerWithPrimaryKey<TId,TReferrerId>:IReferrer<TReferrerId>
        where TId:IEquatable<TId>
        where TReferrerId:IEquatable<TReferrerId>
    {
        TId Id { get;set;}
    }
}
