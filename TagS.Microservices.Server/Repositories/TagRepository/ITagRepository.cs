namespace TagS.Microservices.Server.Repositories.TagRepository
{
    public interface ITagRepository : IRepository<Tag>
    {
        bool Existed(string tagId);
        bool ExistedPreferredName(string preferredTagName);

        Task AddAsync(Tag tag);
        Task AddAsync(IEnumerable<Tag> tags);

        Task<UpdateResult> UpdateAsync(Tag tag);
        /// <summary>
        /// Update many with same change.
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        Task<UpdateResult> UpdateAsync(IEnumerable<string> tags, UpdateDefinition<Tag> updateDefinition);
        /// <summary>
        /// Update many with different changes.
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        Task<BulkWriteResult<Tag>> BulkWriteAsync(IEnumerable<Tag> tags);

        Task<DeleteResult> DeleteAsync(string tagId);
        Task<DeleteResult> DeleteAsync(IEnumerable<string> tagIds);

        Task<Tag> GetTagAsync(string tagId);
    }
}
