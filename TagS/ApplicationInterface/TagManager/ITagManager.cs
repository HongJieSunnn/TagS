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
        Task AddTagAsync(Tag tag);

        //Update methods
        Task AddSynonymToTagAsync(Guid tagGuid,string synonym);
        Task AddSynonymsToTagAsync(Guid tagGuid, IEnumerable<string> synonyms);
        Task AddRelatedTagToTagAsync(Guid tagGuid, Guid relatedTag);
        Task AddRelatedTagsToTagAsync(Guid tagGuid, IEnumerable<Guid> relatedTags);
        Task AddNextTagAsync(Guid tagGuid, Guid nextTagGuid);
        Task AddNextTagsAsync(Guid tagGuid, IEnumerable<Guid> nextTagGuids);
        Task ChangePreferredTagNameAsync(Guid tagGuid, string preferredTagName);
        Task ChangeTagDetailAsync(Guid tagGuid, string tagDetail);

        //Delete methods
        Task DeleteTagAsync(Guid tagGuid);
    }
}
