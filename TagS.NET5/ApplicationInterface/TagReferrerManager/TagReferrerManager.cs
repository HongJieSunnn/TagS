using HongJieSun.TagS.Models.Tags;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.ApplicationInterface.Abstractions;
using TagS.Infrastructure.Repositories.Abstractions;
using TagS.Models.Referrers;
using TagS.Models.Referrers.Abstractions;

namespace TagS.ApplicationInterface.TagReferrerManagers
{
    public  class TagReferrerManager<TReferrerId> : ITagReferrerManager<TReferrerId>
        where TReferrerId : IEquatable<TReferrerId>
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITagReferrerRepository<TReferrerId> _tagReferrerRepository;
        public TagReferrerManager(ITagRepository tagRepository,ITagReferrerRepository<TReferrerId> tagReferrerRepository)
        {
            _tagRepository=tagRepository;
            _tagReferrerRepository = tagReferrerRepository;
        }

        public Task AddAsync(ITagable<TReferrerId> tagable)
        {
            var referrer=GetReferrerFrom(tagable);
            //Get referrer by method GetReferrerFrom.If referrer is not existed in database the return referrer's guid is Guid.Empty.
            //TagReferrerRepository.Existed(IReferrer) method just judge that the guid and need not to access database.
            if (!_tagReferrerRepository.Existed(referrer))
                return _tagReferrerRepository.AddAsync(referrer);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(ITagable<TReferrerId> tagable)
        {
            var referrer = GetReferrerFrom(tagable);

            if (!_tagReferrerRepository.Existed(referrer))
                throw new ArgumentException($"Referrer with ReferrerId{referrer.ReferrerId} is not existed.");

            return _tagReferrerRepository.UpdateAsync(referrer);
        }

        public Task DeleteAsync(ITagable<TReferrerId> tagable)
        {
            var referrer = GetReferrerFrom(tagable);

            if (!_tagReferrerRepository.Existed(referrer))
                throw new ArgumentException($"Referrer with ReferrerId{referrer.ReferrerId} is not existed.");

            return _tagReferrerRepository.DeleteAsync(referrer);
        }

        public async Task AddTagAsync(ITagable<TReferrerId> tagable, TagIdentityModel tagIdentityModel)
        {
            if (!_tagRepository.Existed(tagIdentityModel.TagGuid))
                throw new ArgumentException($"Tag with Guid:{tagIdentityModel.TagGuid} is not existed.");

            var referrer = GetReferrerFrom(tagable);
            if (!_tagReferrerRepository.Existed(referrer))
                throw new ArgumentException($"Referrer with ReferrerId{referrer.ReferrerId} is not existed.");

            referrer.Tags.Add(tagIdentityModel);

            await _tagReferrerRepository.UpdateAsync(referrer);
            await _tagRepository.AddReferrerAsync(tagIdentityModel.TagGuid, referrer.Guid);
        }

        public async Task RemoveTagAsync(ITagable<TReferrerId> tagable, Guid tagGuid)
        {
            if (!_tagRepository.Existed(tagGuid))
                throw new ArgumentException($"Tag with Guid{tagGuid} is not existed.");

            var referrer=GetReferrerFrom(tagable);

            if(!_tagReferrerRepository.Existed(referrer))
                throw new ArgumentException($"Referrer with ReferrerId{referrer.ReferrerId} is not existed.");

            var tagWithGuid=referrer.Tags.FirstOrDefault(r=>r.TagGuid==tagGuid);
            if(tagWithGuid!=null)
                referrer.Tags.Remove(tagWithGuid);

            await _tagReferrerRepository.UpdateAsync(referrer);
            await _tagRepository.RemoveReferrerAsync(tagGuid, referrer.Guid);
        }

        private IReferrer<TReferrerId> GetReferrerFrom(ITagable<TReferrerId> tagable)
        {
            var referrer = tagable.ToReferrer();
            referrer.Guid = Guid.Empty;//Under any situations,The Guid of Referrer which generate by ToReferrer should be Guid.Empty.
            if (IsReferrerValidated(referrer))
            {
                var referrerInDatabase = _tagReferrerRepository.GetReferrerByReferrerId(referrer.ReferrerId);//referrerInDatabase is null explains that this referrer is not existed in database.
                return referrerInDatabase??referrer;//referrer's guid is Guid.Empty.
            }
            else 
                throw new ArgumentException($"{nameof(tagable)} can not convert to a referrer instance.");
        }

        private bool IsReferrerValidated(IReferrer<TReferrerId> referrer)
        {
            if (referrer == null)
                throw new ArgumentNullException($"{nameof(referrer)} can not be null.");

            var referrerType = referrer.GetType().GetGenericTypeDefinition();
            if (!ReferrerStatics.ReferrerTypes.Contains(referrerType))
                return false;

            return true;
        }
    }
}
