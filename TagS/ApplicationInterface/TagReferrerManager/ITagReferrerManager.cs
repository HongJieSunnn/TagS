using HongJieSun.TagS.Models.Tags;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.ApplicationInterface.Abstractions;
using TagS.Models.Referrers.Abstractions;

namespace TagS.ApplicationInterface.TagReferrerManager
{
    internal interface ITagReferrerManager<TReferrerId,TPersistence>
        where TReferrerId : IEquatable<TReferrerId>
    {
        Task AddAsync(ITagable<TReferrerId, TPersistence> tagable);
        Task UpdateAsync(ITagable<TReferrerId,TPersistence> tagable);
        Task DeleteAsync(ITagable<TReferrerId,TPersistence> tagable);
        Task AddTagAsync(ITagable<TReferrerId,TPersistence> tagable, TagIdentityModel tagIdentityModel);
        Task RemoveTagAsync(ITagable<TReferrerId,TPersistence> tagable, Guid tagGuid);
    }
}
