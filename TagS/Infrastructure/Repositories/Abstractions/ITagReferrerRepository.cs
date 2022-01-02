using HongJieSun.TagS.Models.Tags;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers.Generic;

namespace TagS.Infrastructure.Repositories.Abstractions
{
    internal interface ITagReferrerRepository<TPersistence>
    {
        bool Existed<TReferrerId>(IReferrer<TReferrerId> referrer)  where TReferrerId : IEquatable<TReferrerId>;
        bool Existed(ObjectId referrer);
        bool Existed(IEnumerable<ObjectId> referrers);
        void Add<TReferrerId>(IReferrer<TReferrerId> referrer)  where TReferrerId : IEquatable<TReferrerId>;
        int Update<TReferrerId>(IReferrer<TReferrerId> referrer)  where TReferrerId : IEquatable<TReferrerId>;
        void Delete<TReferrerId>(IReferrer<TReferrerId> referrer)  where TReferrerId : IEquatable<TReferrerId>;
        int AddTag<TReferrerId>(IReferrer<TReferrerId> referrer, Tag tag)  where TReferrerId : IEquatable<TReferrerId>;
        int RemoveTag<TReferrerId>(IReferrer<TReferrerId> referrer, Tag tag)  where TReferrerId : IEquatable<TReferrerId>;
        Task AddAsync<TReferrerId>(IReferrer<TReferrerId> referrer)  where TReferrerId : IEquatable<TReferrerId>;
        Task<int> UpdateAsync<TReferrerId>(IReferrer<TReferrerId> referrer)  where TReferrerId : IEquatable<TReferrerId>;
        Task DeleteAsync<TReferrerId>(IReferrer<TReferrerId> referrer)  where TReferrerId : IEquatable<TReferrerId>;
        Task<int> AddTagAsync<TReferrerId>(IReferrer<TReferrerId> referrer, Tag tag)  where TReferrerId : IEquatable<TReferrerId>;
        Task<int> RemoveTagAsync<TReferrerId>(IReferrer<TReferrerId> referrer, Tag tag)  where TReferrerId : IEquatable<TReferrerId>;
    }
}
