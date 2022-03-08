namespace TagS.Microservices.Server.DomainEventHandlers
{
    public class AddTagDomainEventHandler : INotificationHandler<AddTagDomainEvent>
    {
        private readonly ITagWithReferrerRepository _tagWithReferrerRepository;
        public AddTagDomainEventHandler(ITagWithReferrerRepository tagWithReferrerRepository)
        {
            _tagWithReferrerRepository = tagWithReferrerRepository;

        }
        public async Task Handle(AddTagDomainEvent request, CancellationToken cancellationToken)
        {
            await _tagWithReferrerRepository.AddAsync(request.TagWithReferrer);
        }
    }
}
