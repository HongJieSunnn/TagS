namespace TagS.Microservices.Server.Queries.TagWithReferrerQueries
{
    public interface ITagWithReferrerQueries
    {
        Task<IEnumerable<TReferrer>> GetReferrersOfTagAsync<TReferrer>(string tagId, Func<TReferrer, bool> predicate) where TReferrer : IReferrer;
        Task<IEnumerable<TReferrer>> GetReferrersOfTagsAsync<TReferrer>(IEnumerable<string> tagIds, Func<TReferrer, bool> predicate) where TReferrer : IReferrer;
    }
}
