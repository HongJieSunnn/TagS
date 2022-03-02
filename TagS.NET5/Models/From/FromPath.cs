using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Models.From
{
    public class FromPath : From
    {
        public override string FromTypeName => nameof(FromPath);
        public string Path { get; set; }
    }
}
