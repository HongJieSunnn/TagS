using Innermost.MongoDBContext.Configurations;
using MediatR;
using TagS.Microservices.Server.Models;

namespace TagS.Microservices.Server
{
    public class TagSMongoDBContext : MongoDBContextBase, IUnitOfWork
    {
        private bool _disposed;
        private readonly IMediator? _mediator;
        public IMongoCollection<TagWithReferrer>? TagWithReferrers { get; set; }
        public IMongoCollection<Tag>? Tags { get; set; }
        public TagSMongoDBContext(MongoDBContextConfiguration<TagSMongoDBContext> mongoDB) : base(mongoDB)
        {
        }

        public TagSMongoDBContext(MongoDBContextConfiguration<TagSMongoDBContext> mongoDB, IMediator mediator) : base(mongoDB)
        {
            _mediator = mediator;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();//TODO raise domainEvents
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                base.Client.Cluster.Dispose();
                _disposed = true;
            }
        }
    }
}
