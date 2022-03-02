using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers;
using TagS.Models.Referrers.Abstractions;

namespace HongJieSun.TagS.Models.Tags
{
    public class Tag
    {
        public Guid Guid{ get; set; }//if use sql,context generate a shadow primary key.
        public string PreferredTagName { get; set; }//unique name should be like Emotion:Happy,ProgramingLanguage:C#:C#10
        public string TagDetail { get; set; }
        public Guid PreviousTagId { get; set; }
        public HashSet<string> Synonyms { get; set; }
        public HashSet<Guid> RelatedTags { get; set; }
        public HashSet<Guid> NextTags { get; set; }
        public HashSet<Guid> Referrers { get; set; }
        //TODO: add navigation properites with BsonIgnore?Or use TagViewModel to get completed tag info.

        protected Tag()
        {
            Synonyms=new HashSet<string>();
            RelatedTags=new HashSet<Guid>();
            Referrers=new HashSet<Guid>();
        }

        public Tag(Guid guid,[NotNull]string preferredTagName, string tagDetail, Guid previousTagId, HashSet<string> synonyms, HashSet<Guid> relatedTags,HashSet<Guid> nextTags, HashSet<Guid> referrers):this()
        {
            Guid = guid;
            PreferredTagName = preferredTagName;
            TagDetail = tagDetail;
            PreviousTagId = previousTagId;
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
