using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers.Enumeration.MongoDB;

namespace TagS.Models.Referrers
{
    internal static class ReferrerStatics
    {
        public static HashSet<Type> ReferrerTypes = new HashSet<Type>(new List<Type>
        {
            typeof (MongoDBEntityReferrer<>),
            typeof (FileReferrer),
            typeof (UserReferrer<>),
        });
    }
}
