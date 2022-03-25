using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TagS.Microservices.Client.DomainSeedWork;

namespace TagS.Microservices.Client.Models
{
    public class TagSummary
    {
        [Key]
        [Column("TagId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TagId { get; set; }
        [Column("TagName")]
        public string TagName { get; set; }
        public TagSummary(string tagId,string tagName)
        {
            TagId = tagId;
            TagName = tagName;
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
        public List<TEntity>? Entities { get; set; }
        public TagSummary(string tagId, string tagName) : base(tagId, tagName)
        {
        }
    }
}
