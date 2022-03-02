using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Models.From
{
    public class FromSQL : From
    {
        public override string FromTypeName => nameof(FromSQL);
        public string TableName { get; set; }
    }
}
