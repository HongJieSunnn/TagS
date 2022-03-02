using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers.Abstractions;

namespace TagS.ApplicationInterface.Queries.Abstractions
{
    public interface ITagReferrerQueries<TReferrerId>:ITagSQueries
        where TReferrerId : IEquatable<TReferrerId>
    {
        Task<IEnumerable<IReferrer<TReferrerId>>> GetAllReferrersAsync();
        Task<IReferrer<TReferrerId>> GetOneReferrerByReferrerIdAsync(TReferrerId referrerId);
        Task<IEnumerable<IReferrer<TReferrerId>>> GetManyReferrersByReferrerIdsAsync(IEnumerable<TReferrerId> referrerIds);

        //Actually,there may be not neccessary to use these methods under.
        //We can get tags of one referrer after query tagable instances and then use tagable.Id as ReferrerId to find referrer instances and get tags name.
        //When use tag to get referrers of that tag,we may use these methods.But it's same that we can get the referrers' referrerId and then get referrers by users theirselves.
        //For convenience,the under methods will implement later.
        //TODO:GetReferrersByTag?

        Task<dynamic> GetOneReferrerCompletedInfomationsAsync(IReferrer<TReferrerId> referrer);
        Task<IEnumerable<dynamic>> GetManyReferrerCompletedInfomationsAsync(IEnumerable<IReferrer<TReferrerId>> referrer);
    }
}
