using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Microservices.Client.Models;

namespace TagS.Microservices.Server.Models
{
    public class TagWithReferrer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string PreferredTagName { get; set; }
        public string TagDetail { get; set; }
        public HashSet<string> Synonyms { get; set; }
        public List<IReferrer> Referrers { get; set; }
        public TagWithReferrer(string preferredTagName,string tagDetail,IEnumerable<string> synonyms,List<IReferrer> referrers)
        {
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            Synonyms=(synonyms is HashSet<string>)?(HashSet<string>)synonyms:synonyms.ToHashSet();
            Referrers = referrers ?? new List<IReferrer>();
        }
    }
}
