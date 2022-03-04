using TagS.Microservices.Client.Models;

namespace TagS.Microservices.Server.Repositories.TagWithReferrerRepository
{
    public interface ITagWithReferrerRepository
    {
        Task AddAsync(TagWithReferrer tagWithReferrer);
        Task<DeleteResult> DeleteAsync(string tagId);
        Task AddReferrerToTagAsync(string tagId,IReferrer referrer);
        Task RemoveReferrerToTagAsync(string tagId,IReferrer referrer);
        Task ChangeTagDetailAsync(string tagId,string detail);
        Task AddSynonymAsync(string tagId,string synonym);
        Task RemoveSynonymAsync(string tagId,string synonym);
    }
}
