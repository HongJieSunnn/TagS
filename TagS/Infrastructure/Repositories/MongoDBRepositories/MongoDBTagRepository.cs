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
    internal class MongoDBTagRepository<TPersistence>:ITagRepository<TPersistence>
    {
        private readonly MongoDBContextBase _context;
        public MongoDBTagRepository(MongoDBContextBase mongoDBContext)
        {
            _context = mongoDBContext;
        }

        public void Add(Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Tag tag)
        {
            throw new NotImplementedException();
        }

        public void AddReferrer(Tag tag, ObjectId referrerObjectId)
        {
            throw new NotImplementedException();
        }

        public Task AddReferrerAsync(Tag tag, ObjectId referrerObjectId)
        {
            throw new NotImplementedException();
        }

        public void Delete(Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Tag tag)
        {
            throw new NotImplementedException();
        }

        public bool Existed(Tag tag)
        {
            throw new NotImplementedException();
        }

        public void Update(Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}
