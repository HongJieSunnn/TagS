using HongJieSun.TagS.Models.Tags;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.ApplicationInterface.Abstractions;
using TagS.Models.Referrers.Abstractions;

namespace TagS.ApplicationInterface.TagReferrerManagers
{
    public interface ITagReferrerManager<TReferrerId>
        where TReferrerId : IEquatable<TReferrerId>
    {
        Task AddAsync(ITagable<TReferrerId> tagable);
        Task UpdateAsync(ITagable<TReferrerId> tagable);
        Task DeleteAsync(ITagable<TReferrerId> tagable);
        Task AddTagAsync(ITagable<TReferrerId> tagable, TagIdentityModel tagIdentityModel);
        Task RemoveTagAsync(ITagable<TReferrerId> tagable, Guid tagGuid);
    }
}
