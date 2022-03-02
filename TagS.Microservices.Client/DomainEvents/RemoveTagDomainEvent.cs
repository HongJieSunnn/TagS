using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Microservices.Client.DomainEvents
{
    public class RemoveTagDomainEvent:INotification
    {
        public IReferrer Referrer { get; private set; }
        public string TagId { get; private set; }
        public RemoveTagDomainEvent(IReferrer referrer, string tagId)
        {
            Referrer = referrer;
            TagId = tagId;
        }
    }
}
