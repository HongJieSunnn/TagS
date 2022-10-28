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
            var tagToAdd = new Tag(null, notification.PreferredTagName, notification.TagDetail, notification.PreviousTagId, notification.Ancestors, notification.Synonyms, null);
            await _tagRepository.AddAsync(tagToAdd);
        }
    }
}
