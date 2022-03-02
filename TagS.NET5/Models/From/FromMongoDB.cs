using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagS.Models.From
{
    public class FromMongoDB : From
    {
        public override string FromTypeName => nameof(FromMongoDB);
        /// <summary>
        ///TODO not store the connection string.User should use the connection string to configure FromSourceContext 
        /// </summary>
        public string FromMongoDBDatabase { get; set; }
        public string FromMongoDBCollection { get; set; }
    }
}
