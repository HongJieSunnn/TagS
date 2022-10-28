namespace TagS.Microservices.Server.Repositories.TagWithReferrerRepository
{
    public interface ITagWithReferrerRepository
    {
        Task AddAsync(TagWithReferrer tagWithReferrer);
        Task<UpdateResult> UpdateAsync(string tagId, UpdateDefinition<TagWithReferrer> updateDefinition, params FilterDefinition<TagWithReferrer>[] filterDefinitions);
        Task<UpdateResult> DeleteAsync(string tagId);
        Task AddReferrerToTagAsync(string tagId, IReferrer referrer);
        Task RemoveReferrerToTagAsync(string tagId, IReferrer referrer);
    }
}
