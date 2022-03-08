namespace TagS.Microservices.Server.Repositories.TagReviewedRepository
{
    public interface ITagReviewedRepository:IRepository<TagReviewed>
    {
        Task CreateReviewedTagAsync(TagReviewed tagReviewed);
        Task<bool> PassReviewedTagAsync(string tagReviewedId);
        Task<bool> RefuseReviewedTagAsync(string tagReviewedId);
        Task<TagReviewed> GetTagReviewedAsync(string tagReviewedId);
    }
}
