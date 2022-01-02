using HongJieSun.TagS.Models.Tags;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.ApplicationInterface.TagManager
{
    public interface ITagManager<TPersistence>
    {
        Task<bool> AddTagAsync(Tag tag);

        Task<bool> AddSynonymToTagAsync(Tag tag,string synonym);
        Task<bool> AddSynonymsToTagAsync(Tag tag,IEnumerable<string> synonym);

        Task<bool> AddRelatedTagToTagAsync(Tag tag,string relatedTag);
        Task<bool> AddRelatedTagsToTagAsync(Tag tag,IEnumerable<string> relatedTag);

        /// <summary>
        /// Generally,referrer should be added by Tagable.But TagManager should have that power to do this action.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="ReferrerId"></param>
        /// <returns></returns>
        Task<bool> AddExitedReferrerToTagAsync(Tag tag, ObjectId ReferrerId);
        Task<bool> AddExitedReferrerToTagAsync(Tag tag, IEnumerable<ObjectId> ReferrerId);

        Task<bool> AddNextTagAsync(Tag tag,string nextTag);
        Task<bool> AddNextTagsAsync(Tag tag,IEnumerable<string> nextTags);

        Task<bool> ChangePreferredTagNameAsync(Tag tag,string preferredTagName);

        Task<bool> ChangeTagDetailAsync(Tag tag, string tagDetail);

        Task<bool> RemoveTagAsync(Tag tag);
    }
}
