﻿namespace TagS.Microservices.Server.Queries.TagReviewedQueries
{
    public interface ITagReviewedQueries
    {
        Task<IEnumerable<TagReviewedDTO>> GetTobeReviewedTagsAsync();
    }
}
