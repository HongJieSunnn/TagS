﻿using HongJieSun.TagS.Models.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.ApplicationInterface.Queries.Abstractions
{
    public interface ITagQueries:ITagSQueries
    {
        Task<IEnumerable<Tag>> GetAllTagsAsync();
        Task<Tag> GetTagByGuidAsync(Guid tagGuid);
        Task<Tag> GetTagByPreferredNameAsync(string preferredName);
        Task<Tag> GetTagBySynonymAsync(string synonym);
    }
}
