using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Microservices.Server.Queries.TagQueries
{
    public interface ITagQueries
    {
        Task<IEnumerable<Tag>> GetAllTagsAsync();
        Task<IEnumerable<Tag>> GetAllFirstLevelTagsAsync();
        Task<IEnumerable<Tag>> GetNextTagsAsync(string objectId);
        Task<Tag> GetTagByPreferredNameAsync(string preferredName);
        Task<Tag> GetTagBySynonymAsync(string synonym);
    }
}
