using HongJieSun.TagS.Models.Tags;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Infrastructure.Repositories.Abstractions;

namespace TagS.ApplicationInterface.TagManager
{
    /// <summary>
    /// TagManager is to manage just tags.
    /// </summary>
    internal class TagManager<TPersistence> : ITagManager<TPersistence>
    {
        private readonly ITagRepository<TPersistence> _tagRepository;
        public TagManager(ITagRepository<TPersistence> tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public Task AddTagAsync(Tag tag)
        {
            if (_tagRepository.Existed(tag.PreferredTagName))
                throw new ArgumentException($"Tag with PreferredTagName{tag.PreferredTagName} has already existed.");

            return _tagRepository.AddAsync(tag);
        }

        public async Task AddNextTagAsync(Guid tagGuid, Guid nextTagGuid)
        {
            if (!_tagRepository.Existed(tagGuid))
                throw new ArgumentException($"Tag with Guid{tagGuid} is not existed");
            if(!_tagRepository.Existed(nextTagGuid))
                throw new ArgumentException($"NextTag with Guid{nextTagGuid} is not existed");

            var tag=await _tagRepository.GetTagByGuidAsync(tagGuid);
            tag.NextTags.Add(nextTagGuid);
            await _tagRepository.UpdateAsync(tag);
        }

        public async Task AddNextTagsAsync(Guid tagGuid, IEnumerable<Guid> nextTagGuids)
        {
            if (!_tagRepository.Existed(tagGuid))
                throw new ArgumentException($"Tag with Guid{tagGuid} is not existed");
            if (!_tagRepository.Existed(nextTagGuids))
                throw new ArgumentException($"There is at least one tag is not existed whose guid is in param nextTagGuids");

            var tag = await _tagRepository.GetTagByGuidAsync(tagGuid);
            tag.NextTags.Union(nextTagGuids);//TODO:maybe there should be foreach(nexttag in ...) tag.NextTags.Add(nexttag)
            await _tagRepository.UpdateAsync(tag);
        }

        public async Task AddRelatedTagToTagAsync(Guid tagGuid, Guid relatedTag)
        {
            if (!_tagRepository.Existed(tagGuid))
                throw new ArgumentException($"Tag with Guid{tagGuid} is not existed");
            if (!_tagRepository.Existed(relatedTag))
                throw new ArgumentException($"NextTag with Guid{relatedTag} is not existed");

            var tag =await _tagRepository.GetTagByGuidAsync(tagGuid);
            tag.RelatedTags.Add(relatedTag);
            await _tagRepository.UpdateAsync(tag);
        }

        public async Task AddRelatedTagsToTagAsync(Guid tagGuid, IEnumerable<Guid> relatedTags)
        {
            if (!_tagRepository.Existed(tagGuid))
                throw new ArgumentException($"Tag with Guid{tagGuid} is not existed");
            if (!_tagRepository.Existed(relatedTags))
                throw new ArgumentException($"There is at least one tag is not existed whose guid is in param nextTagGuids");

            var tag = await _tagRepository.GetTagByGuidAsync(tagGuid);
            tag.RelatedTags.Union(relatedTags);
            await _tagRepository.UpdateAsync(tag);
        }

        public async Task AddSynonymToTagAsync(Guid tagGuid, string synonym)
        {
            if (!_tagRepository.Existed(tagGuid))
                throw new ArgumentException($"Tag with Guid{tagGuid} is not existed");

            var tag = await _tagRepository.GetTagByGuidAsync(tagGuid);
            tag.Synonyms.Add(synonym);
            await _tagRepository.UpdateAsync(tag);
        }

        public async Task AddSynonymsToTagAsync(Guid tagGuid, IEnumerable<string> synonyms)
        {
            if (!_tagRepository.Existed(tagGuid))
                throw new ArgumentException($"Tag with Guid{tagGuid} is not existed");

            var tag = await _tagRepository.GetTagByGuidAsync(tagGuid);
            tag.Synonyms.Union(synonyms);
            await _tagRepository.UpdateAsync(tag);
        }

        public async Task ChangePreferredTagNameAsync(Guid tagGuid, string preferredTagName)
        {
            if (!_tagRepository.Existed(tagGuid))
                throw new ArgumentException($"Tag with Guid{tagGuid} is not existed");
            if (_tagRepository.Existed(preferredTagName))
                throw new ArgumentException($"Tag with PreferredTagName{preferredTagName} has already existed");

            var tag =await _tagRepository.GetTagByGuidAsync(tagGuid);
            tag.PreferredTagName=preferredTagName;
            await _tagRepository.UpdateAsync(tag);
        }

        public async Task ChangeTagDetailAsync(Guid tagGuid, string tagDetail)
        {
            if (!_tagRepository.Existed(tagGuid))
                throw new ArgumentException($"Tag with Guid{tagGuid} is not existed");

            var tag = await _tagRepository.GetTagByGuidAsync(tagGuid);
            tag.TagDetail = tagDetail;
            await _tagRepository.UpdateAsync(tag);
        }

        public Task DeleteTagAsync(Guid tagGuid)
        {
            if (!_tagRepository.Existed(tagGuid))
                throw new ArgumentException($"Tag with Guid{tagGuid} is not existed");

            return _tagRepository.DeleteAsync(tagGuid);
        }
    }
}
