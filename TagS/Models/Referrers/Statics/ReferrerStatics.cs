using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Models.Referrers
{
    internal static class ReferrerStatics
    {
        public static HashSet<Type> ReferrerTypes = new HashSet<Type>(new List<Type>
        {
            typeof (EntityReferrer<>),
            typeof (FileReferrer),
            typeof (UserReferrer<>),
        });
    }
}
