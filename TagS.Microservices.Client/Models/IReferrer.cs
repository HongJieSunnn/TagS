using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Microservices.Client.Models
{
    public interface IReferrer
    {
        /// <summary>
        /// We use referrerName to classify.
        /// For example,We want get LifeRecord with tag Emotion:Happy.We get all referrer with referrerName LifeRecord.
        /// </summary>
        string ReferrerName { get; }
    }
}
