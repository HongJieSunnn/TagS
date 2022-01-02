using HongJieSun.TagS.Models;
using HongJieSun.TagS.Models.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers;
using TagS.Models.Referrers.Generic;

namespace TagS.ApplicationInterface.Abstractions
{
    /// <summary>
    /// To identify a class that can tag.
    /// </summary>
    /// <typeparam name="TPersistence">Persistence modes:SQL MongoDB File.</typeparam>
    public interface ITagable<TReferrerId,TPersistence> 
        where TReferrerId : IEquatable<TReferrerId>
    {
        IReferrer<TReferrerId> ToReferrer();
    }
}
