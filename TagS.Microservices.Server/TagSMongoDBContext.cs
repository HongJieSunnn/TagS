using Innermost.MongoDBContext.Configurations;

namespace TagS.Microservices.Server
{
    public class TagSMongoDBContext : MongoDBContextBase, IUnitOfWork
    {
        private bool _disposed;
        private readonly IMediator _mediator;
        public IMongoCollection<TagWithReferrer>? TagWithReferrers { get; set; }
        public IMongoCollection<Tag>? Tags { get; set; }
        public IMongoCollection<TagReviewed>? TagRevieweds { get; set; }
        public TagSMongoDBContext(MongoDBContextConfiguration<TagSMongoDBContext> mongoDB) : base(mongoDB)
        {
        }

        public TagSMongoDBContext(MongoDBContextConfiguration<TagSMongoDBContext> mongoDB, IMediator mediator) : base(mongoDB)
        {
            _mediator = mediator;
            CreateIndexes();
        }

        public async Task<bool> SaveEntitiesAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : Entity<string>
        {
            if(entity.DomainEvents is not null)
            {
                var domainEvents = entity.DomainEvents;

                foreach (var domainEvent in domainEvents)
                {
                    await _mediator.Publish(domainEvent);
                }

                entity.ClearDomainEvents();//reference
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

        private void CreateIndexes()
        {
            CreateIndexesForTag();
            CreateIndexesForTagReviewed();
            CreateIndexesForTagWithReferrers();
        }

        private void CreateIndexesForTagWithReferrers()
        {
            if(!TagWithReferrers!.Indexes.List().Any())
            {
                var ancestorsIndex = Builders<TagWithReferrer>.IndexKeys.Ascending(t => t.Ancestors);
                var referrersIndex= Builders<TagWithReferrer>.IndexKeys.Ascending(t => t.Referrers);

                var createIndexModels = new[] { ancestorsIndex, referrersIndex }.Select(i => new CreateIndexModel<TagWithReferrer>(i));

                TagWithReferrers.Indexes.CreateManyAsync(createIndexModels).GetAwaiter().GetResult();
            }
        }

        private void CreateIndexesForTag()
        {
            if (!Tags!.Indexes.List().Any())
            {
                var ancestorsIndex = Builders<Tag>.IndexKeys.Ascending("_ancestors");
                var preIdIndex = Builders<Tag>.IndexKeys.Ascending(t => t.PreviousTagId);
                var nameIndex = Builders<Tag>.IndexKeys.Ascending(t => t.PreferredTagName);
                var synonymsIndex = Builders<Tag>.IndexKeys.Ascending("_synonyms");
                var tagDetailIndex = Builders<Tag>.IndexKeys.Text(t => t.TagDetail);

                var createIndexModels = new[] { ancestorsIndex, preIdIndex,nameIndex,synonymsIndex,tagDetailIndex }.Select(i => new CreateIndexModel<Tag>(i));

                Tags.Indexes.CreateManyAsync(createIndexModels).GetAwaiter().GetResult();
            }
        }

        private void CreateIndexesForTagReviewed()
        {
            if (!TagRevieweds!.Indexes.List().Any())
            {
                var nameIndex = Builders<TagReviewed>.IndexKeys.Ascending(t => t.PreferredTagName);
                var statueIndex = Builders<TagReviewed>.IndexKeys.Ascending(t => t.Statue);

                var createIndexModels = new[] { nameIndex,statueIndex }.Select(i => new CreateIndexModel<TagReviewed>(i));

                TagRevieweds.Indexes.CreateManyAsync(createIndexModels).GetAwaiter().GetResult();
            }
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default,bool saveChanges=true)
        {
            throw new NotImplementedException();
        }
    }
}
