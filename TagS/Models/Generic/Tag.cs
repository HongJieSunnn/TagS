using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers;

namespace TagS.Models.Generic
{
    public class Tag<TReferrer> where TReferrer : IReferrer
    {
        public string PreferredTagName { get; set; }
        public string TagDetail { get; set; }
        public HashSet<string> Synonyms { get; set; }
        public HashSet<string> RelatedTags { get; set; }
        public HashSet<TReferrer> Referrers { get; set; }

        protected Tag()
        {
            Synonyms=new HashSet<string>();
            RelatedTags=new HashSet<string>();
            Referrers=new HashSet<TReferrer>();
        }

        public Tag([NotNull]string preferredTagName, string tagDetail, HashSet<string> synonyms, HashSet<string> relatedTags, HashSet<TReferrer> referrers):this()
        {
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            Synonyms = synonyms;
            RelatedTags = relatedTags;
            Referrers = referrers;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Tag<TReferrer>)) return false;

            var completedObj=obj as Tag<TReferrer>;
            return this.PreferredTagName.Equals(completedObj!.PreferredTagName);
        }

        public override int GetHashCode()
        {
            return PreferredTagName.GetHashCode();
        }
    }
}
