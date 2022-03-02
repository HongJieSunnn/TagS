using HongJieSun.TagS.Models.Tags;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers.Abstractions;

namespace TagS.Infrastructure.Repositories.Abstractions
{
    public interface ITagReferrerRepository<TReferrerId>
        where TReferrerId : IEquatable<TReferrerId>
    {
        bool Existed(IReferrer<TReferrerId> referrer);
        bool Existed(Guid referrerGuid);
        bool Existed(IEnumerable<Guid> referrerGuids);
        IReferrer<TReferrerId> GetReferrerByGuid(Guid referrerGuid);
        IReferrer<TReferrerId> GetReferrerByReferrerId(TReferrerId referrerId);
        Task<IReferrer<TReferrerId>> GetReferrerByGuidAsync(Guid referrerGuid);
        Task<IReferrer<TReferrerId>> GetReferrerByReferrerIdAsync(TReferrerId referrerId);
        Task AddAsync(IReferrer<TReferrerId> referrer);
        Task UpdateAsync(IReferrer<TReferrerId> referrer);
        Task DeleteAsync(IReferrer<TReferrerId> referrer);
    }
}
