namespace TagS.Microservices.Server.DomainEventHandlers.TagReviewed
{
    public class SetReviewedTagPassedDomainEventHandler : INotificationHandler<SetReviewedTagPassedDomainEvent>
    {
        private readonly ITagRepository _tagRepository;
        public SetReviewedTagPassedDomainEventHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;

        }
        public async Task Handle(SetReviewedTagPassedDomainEvent notification, CancellationToken cancellationToken)
        {
            if(notification.PreviousTagId is not null&&notification.FirstLevelTagId is not null)
            {
                //get first level tag to update.
                //Because the tags which are not first level tag is nested in first level tag.If we want to update them,we should update the first level tag of those tags.
                var firstLevelTag = await _tagRepository.GetTagAsync(notification.FirstLevelTagId);
                var preTag = GetPreviousTagOfTagToAdd(firstLevelTag, notification.PreviousTagId)!;
                preTag.AddNextTag(new Tag(null, notification.PreferredTagName, notification.TagDetail, notification.PreviousTagId, null, null, null),notification.FirstLevelTagId);
                await _tagRepository.UpdateAsync(firstLevelTag);
                await _tagRepository.UnitOfWork.SaveEntitiesAsync(preTag, cancellationToken);//To publish AddTagDomainEvent
                return;
            }
            else if(notification.PreviousTagId is not null||notification.FirstLevelTagId is not null)
            {
                throw new ArgumentNullException("PreviousTagId and FirstLevelTagId of reviewed tag must not be null both.");
            }

            var tag = new Tag(null, notification.PreferredTagName, notification.TagDetail, notification.PreviousTagId, null, null, null);
            await _tagRepository.AddAsync(tag);
            await _tagRepository.UnitOfWork.SaveEntitiesAsync(tag, cancellationToken);//To publish AddTagDomainEvent
        }

        private Tag? GetPreviousTagOfTagToAdd(Tag tag,string previousTagId)
        {
            //DFS
            if (tag.Id == previousTagId)
                return tag;
            Tag? tagToGet=null;
            foreach (var nextTag in tag.NextTags)
            {
                tagToGet = GetPreviousTagOfTagToAdd(nextTag, previousTagId);
                if (tagToGet?.Id == previousTagId)
                    return tagToGet;
            }
            return tagToGet;
        }
    }
}
