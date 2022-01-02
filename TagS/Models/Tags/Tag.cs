using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers;
using TagS.Models.Referrers.Generic;

namespace HongJieSun.TagS.Models.Tags
{
    public class Tag
    {
        public string PreferredTagName { get; set; }
        public string TagDetail { get; set; }
        public string PreviousTag { get; set; }
        public HashSet<string> Synonyms { get; set; }
        public HashSet<string> RelatedTags { get; set; }
        public HashSet<string> NextTags { get; set; }
        public HashSet<ObjectId> Referrers { get; set; }

        protected Tag()
        {
            Synonyms=new HashSet<string>();
            RelatedTags=new HashSet<string>();
            Referrers=new HashSet<ObjectId>();
        }

        public Tag([NotNull]string preferredTagName, string tagDetail,string previousTag, HashSet<string> synonyms, HashSet<string> relatedTags,HashSet<string> nextTags, HashSet<ObjectId> referrers):this()
        {
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            PreviousTag = previousTag;
            Synonyms = synonyms;
            RelatedTags = relatedTags;
            NextTags = nextTags;
            Referrers = referrers;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Tag)) return false;

            var completedObj=obj as Tag;
            return this.PreferredTagName.Equals(completedObj!.PreferredTagName);
        }

        public override int GetHashCode()
        {
            return PreferredTagName.GetHashCode();
        }
    }
}
