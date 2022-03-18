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
            if(notification.PreviousTagId is null)
            {
                var tag = new Tag(null, notification.PreferredTagName, notification.TagDetail, notification.PreviousTagId, null, null, null);
                await _tagRepository.AddAsync(tag);
                await _tagRepository.UnitOfWork.SaveEntitiesAsync(tag, cancellationToken);//To publish AddTagDomainEvent
                return;
            }

            var preTag=await _tagRepository.GetTagAsync(notification.PreviousTagId);
            preTag.AddNextTag(new Tag(null, notification.PreferredTagName, notification.TagDetail, notification.PreviousTagId, null, null, null));
            await _tagRepository.UpdateAsync(preTag);
            await _tagRepository.UnitOfWork.SaveEntitiesAsync(preTag,cancellationToken);//To publish AddTagDomainEvent
        }
    }
}
