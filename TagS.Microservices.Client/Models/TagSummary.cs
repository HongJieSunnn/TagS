namespace TagS.Microservices.Client.Models
{
    public class TagSummary
    {
        [BsonId]
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
    }
}
