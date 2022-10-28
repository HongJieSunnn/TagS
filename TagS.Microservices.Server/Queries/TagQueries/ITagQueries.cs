namespace TagS.Microservices.Server.Queries.TagQueries
{
    public interface ITagQueries
    {
        Task<IEnumerable<TagDTO>> GetAllTagsAsync();
        Task<IEnumerable<TagDTO>> GetAllFirstLevelTagsAsync();
        Task<IEnumerable<TagDTO>> GetNextTagsAsync(string tagId);
        Task<TagDTO> GetTagByPreferredNameAsync(string preferredName);
        Task<TagDTO> GetTagBySynonymAsync(string synonym);
    }
}
