using Innermost.MongoDBContext.Configurations;

namespace TagS.Microservices.Server
{
    public class TagSMongoDBContext : MongoDBContextBase, IUnitOfWork
    {
        private bool _disposed;
        private readonly IMediator? _mediator;
        public IMongoCollection<TagWithReferrer>? TagWithReferrers { get; set; }
        public IMongoCollection<Tag>? Tags { get; set; }
        public IMongoCollection<TagReviewed>? TagReviedweds { get; set; }
        public TagSMongoDBContext(MongoDBContextConfiguration<TagSMongoDBContext> mongoDB) : base(mongoDB)
        {
        }

        public TagSMongoDBContext(MongoDBContextConfiguration<TagSMongoDBContext> mongoDB, IMediator mediator) : base(mongoDB)
        {
            _mediator = mediator;
        }

        public async Task<bool> SaveEntitiesAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : Entity<string>
        {
            var domainEvents = entity.DomainEvents;
            entity.ClearDomainEvents();

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }

            return true;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                base.Client.Cluster.Dispose();
                _disposed = true;
            }
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
