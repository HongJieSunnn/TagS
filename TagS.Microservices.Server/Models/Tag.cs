using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Microservices.Server.Models
{
    public class Tag
    {
        [BsonId]
        [BsonIgnore]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string PreferredTagName { get; set; }//unique name should be like Emotion:Happy,ProgramingLanguage:C#:C#10
        public string TagDetail { get; set; }
        public string? PreviousTagId { get; set; }
        public List<string> Synonyms { get; set; }
        public List<string> RelatedTagIds { get; set; }
        public List<Tag> NextTags { get; set; }
        public Tag(string id,string preferredTagName,string tagDetail,string previousTagId,List<string> synonyms,List<string> realatedTagIds,List<Tag> nextTags)
        {
            Id = id;
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            PreviousTagId = previousTagId;
            Synonyms= synonyms;
            RelatedTagIds= realatedTagIds;
            NextTags = nextTags;
        }
    }
}
