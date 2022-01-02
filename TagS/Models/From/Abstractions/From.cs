using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Models.From
{
    /// <summary>
    /// IFrom identify the source of referrer.
    /// </summary>
    public abstract class From
    {
        string FromTypeName { get; }
    }
}
