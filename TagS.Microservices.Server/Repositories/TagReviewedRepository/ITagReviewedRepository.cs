namespace TagS.Microservices.Server.Repositories.TagReviewedRepository
{
    public interface ITagReviewedRepository:IRepository<TagReviewed>
    {
        bool ExistedPreferredName(string preferredName);
        Task CreateReviewedTagAsync(TagReviewed tagReviewed);
        Task<(bool result, TagReviewed? entity)> PassReviewedTagAsync(string tagReviewedId);
        Task<(bool result, TagReviewed? entity)> RefuseReviewedTagAsync(string tagReviewedId);
        Task<TagReviewed> GetTagReviewedAsync(string tagReviewedId);
    }
}
