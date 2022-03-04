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
        public string PreferredTagName { get;set; }
        public string TagDetail { get; set; }
        public List<string> Synonyms { get; set; }
        public List<IReferrer> Referrers { get; set; }
        public TagWithReferrer(string id,string preferredTagName,string tagDetail,List<string> synonyms,List<IReferrer>? referrers)
        {
            Id= id;
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            Synonyms = synonyms;
            Referrers = referrers ?? new List<IReferrer>();
        }
    }
}
