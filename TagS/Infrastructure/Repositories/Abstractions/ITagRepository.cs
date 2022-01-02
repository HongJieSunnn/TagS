using HongJieSun.TagS.Models.Tags;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers.Abstractions;
using Tag= HongJieSun.TagS.Models.Tags.Tag;

namespace TagS.Infrastructure.Repositories.Abstractions
{
    internal interface ITagRepository<TPersistence>
    {
        bool Existed(Tag tag);
        bool Existed(Guid tagGuid);
        bool Existed(IEnumerable<Guid> tagGuids);
        bool Existed(string preferredTagName);
        Task<Tag> GetTagByGuidAsync(Guid tagGuid);
        Task<Tag> GetTagByPreferredNameAsync(string preferredName);
        Task AddAsync(Tag tag);


        Task UpdateAsync(Tag tag);
        /// <summary>
        /// While add tag to referrer.referrer should be also add to tag.So these two methods under will just be called in TagReferrerManager.
        /// </summary>
        Task AddReferrerAsync(Guid tagGuid, Guid referrerGuid);
        Task RemoveReferrerAsync(Guid tagGuid, Guid referrerGuid);


        Task DeleteAsync(Tag tag);
        Task DeleteAsync(Guid tagGuid);
    }
}
