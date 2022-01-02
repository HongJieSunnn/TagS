using HongJieSun.TagS.Models.Tags;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Implementation.Commands;
using TagS.Implementation.Commands.TagCommands;
using TagS.Infrastructure.Repositories.Abstractions;

namespace TagS.ApplicationInterface.TagManager
{
    /// <summary>
    /// TagManager is to manage just tags.
    /// </summary>
    internal class TagManager<TPersistence> : ITagManager<TPersistence>
    {
        private readonly IMediator _mediator;
        public TagManager(IMediator mediator)
        {
            _mediator =  mediator;
        }

        public async Task<bool> AddNextTagAsync(Tag tag, string nextTag)
        {
            tag.NextTags.Add(nextTag);
            var updateTagCommand=new UpdateTagCommand<TPersistence>(tag);
            var statue=await _mediator.Send(updateTagCommand);
            return statue;
        }

        public async Task<bool> AddNextTagsAsync(Tag tag, IEnumerable<string> nextTags)
        {
            tag.NextTags.UnionWith(nextTags);
            var updateTagCommand = new UpdateTagCommand<TPersistence>(tag);
            var statue = await _mediator.Send(updateTagCommand);
            return statue;
        }

        public async Task<bool> AddExitedReferrerToTagAsync(Tag tag, ObjectId ReferrerId)
        {
            var addReferrerToTagCommand=new ReferrerAddedTagEvent<TPersistence>(tag, ReferrerId);
            return await _mediator.Send(addReferrerToTagCommand);
        }

        public async Task<bool> AddExitedReferrerToTagAsync(Tag tag, IEnumerable<ObjectId> ReferrerId)
        {
            var addReferrerToTagCommand = new ReferrerAddedTagEvent<TPersistence>(tag, ReferrerId);
            return await _mediator.Send(addReferrerToTagCommand);
        }

        public async Task<bool> AddRelatedTagToTagAsync(Tag tag, string relatedTag)
        {
            tag.RelatedTags.Add(relatedTag);
            var updateCommand = new UpdateTagCommand<TPersistence>(tag);
            return await _mediator.Send(updateCommand);
        }

        public async Task<bool> AddRelatedTagsToTagAsync(Tag tag, IEnumerable<string> relatedTag)
        {
            tag.RelatedTags.UnionWith(relatedTag);
            var updateCommand=new UpdateTagCommand<TPersistence>(tag);
            return await _mediator.Send(updateCommand);
        }

        public async Task<bool> AddSynonymToTagAsync(Tag tag, string synonym)
        {
            tag.Synonyms.Add(synonym);
            var updateCommand=new UpdateTagCommand<TPersistence>(tag);
            return await _mediator.Send(updateCommand);
        }

        public async Task<bool> AddSynonymsToTagAsync(Tag tag, IEnumerable<string> synonym)
        {
            tag.Synonyms.UnionWith(synonym);
            var updateCommand = new UpdateTagCommand<TPersistence>(tag);
            return await _mediator.Send(updateCommand);
        }

        public async Task<bool> AddTagAsync(Tag tag)
        {
            var addTagCommand=new AddTagCommand<TPersistence>(tag);
            return await _mediator.Send(addTagCommand);
        }

        public async Task<bool> ChangePreferredTagNameAsync(Tag tag, string preferredTagName)
        {
            var changePreferredTagNameCommand=new ChangePreferredTagNameCommand<TPersistence>(tag, preferredTagName);
            return await _mediator.Send(changePreferredTagNameCommand);
        }

        public async Task<bool> ChangeTagDetailAsync(Tag tag, string tagDetail)
        {
            tag.TagDetail = tagDetail;
            var updateCommand=new UpdateTagCommand<TPersistence>(tag);
            return await _mediator.Send(updateCommand);
        }

        public async Task<bool> RemoveTagAsync(Tag tag)
        {
            var removeCommand=new RemoveTagCommand<TPersistence>(tag);
            return await _mediator.Send(removeCommand);
        }
    }
}
