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
    internal interface ITagRepository<TPersistence>
    {
        bool Existed(Tag tag);
        void Add(Tag tag);
        void Update(Tag tag);
        void Delete(Tag tag);
        void AddReferrer(Tag tag, ObjectId referrerObjectId);
        Task AddAsync(Tag tag);
        Task UpdateAsync(Tag tag);
        Task DeleteAsync(Tag tag);
        Task AddReferrerAsync(Tag tag, ObjectId referrerObjectId);
    }
}
