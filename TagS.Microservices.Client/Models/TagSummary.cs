using TagS.Microservices.Client.DomainSeedWork;

namespace TagS.Microservices.Client.Models
{
    public class TagSummary
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string TagId { get; set; }
        public string Name { get; set; }
        public TagSummary(string tagId,string name)
        {
            TagId = tagId;
            Name = name;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            TagSummary other = obj as TagSummary;
            return this.TagId.Equals(other.TagId);
        }

        public override int GetHashCode()
        {
            return TagId.GetHashCode();
        }
    }

    /// <summary>
    /// TagSummary<TEntity> is used for SQLEntity and contains ICollection<TEntity> Entities as navigation property.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class TagSummary<TId,TEntity> : TagSummary
        where TEntity: TagableEntity<TId, TEntity>
        where TId : IEquatable<TId>, IComparable<TId>
    {
        public ICollection<TEntity>? Entities { get; set; }
        public TagSummary(string tagId, string name) : base(tagId, name)
        {
        }
    }
}
