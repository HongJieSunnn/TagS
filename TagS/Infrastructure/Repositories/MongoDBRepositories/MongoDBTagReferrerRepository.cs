using HongJieSun.TagS.Models.Tags;
using Innermost.MongoDBContext;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Infrastructure.Repositories.Abstractions;
using TagS.Models.Referrers.Generic;

namespace TagS.Infrastructure.Repositories.MongoDBRepositories
{
    internal class MongoDBTagReferrerRepository<TPersistence> :ITagReferrerRepository<TPersistence>
    {
        private readonly MongoDBContextBase _context;
        public MongoDBTagReferrerRepository(MongoDBContextBase mongoDBContext)
        {
            _context = mongoDBContext;
        }

        public void Add<TReferrerId>(IReferrer<TReferrerId> referrer) where TReferrerId : IEquatable<TReferrerId>
        {
            throw new NotImplementedException();
        }

        public Task AddAsync<TReferrerId>(IReferrer<TReferrerId> referrer) where TReferrerId : IEquatable<TReferrerId>
        {
            throw new NotImplementedException();
        }

        public int AddTag<TReferrerId>(IReferrer<TReferrerId> referrer, Tag tag) where TReferrerId : IEquatable<TReferrerId>
        {
            throw new NotImplementedException();
        }

        public Task<int> AddTagAsync<TReferrerId>(IReferrer<TReferrerId> referrer, Tag tag) where TReferrerId : IEquatable<TReferrerId>
        {
            throw new NotImplementedException();
        }

        public void Delete<TReferrerId>(IReferrer<TReferrerId> referrer) where TReferrerId : IEquatable<TReferrerId>
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync<TReferrerId>(IReferrer<TReferrerId> referrer) where TReferrerId : IEquatable<TReferrerId>
        {
            throw new NotImplementedException();
        }

        public bool Existed<TReferrerId>(IReferrer<TReferrerId> referrer) where TReferrerId : IEquatable<TReferrerId>
        {
            throw new NotImplementedException();
        }

        public bool Existed(ObjectId referrer)
        {
            throw new NotImplementedException();
        }

        public bool Existed(IEnumerable<ObjectId> referrers)
        {
            throw new NotImplementedException();
        }

        public int RemoveTag<TReferrerId>(IReferrer<TReferrerId> referrer, Tag tag) where TReferrerId : IEquatable<TReferrerId>
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveTagAsync<TReferrerId>(IReferrer<TReferrerId> referrer, Tag tag) where TReferrerId : IEquatable<TReferrerId>
        {
            throw new NotImplementedException();
        }

        public int Update<TReferrerId>(IReferrer<TReferrerId> referrer) where TReferrerId : IEquatable<TReferrerId>
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync<TReferrerId>(IReferrer<TReferrerId> referrer) where TReferrerId : IEquatable<TReferrerId>
        {
            throw new NotImplementedException();
        }
    }
}
