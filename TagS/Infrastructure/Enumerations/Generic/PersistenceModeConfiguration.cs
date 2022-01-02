using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Infrastructure.Configurations;

namespace TagS.Infrastructure.Enumerations.Generic
{
    public class PersistenceModeConfiguration<TOptions>
    {
        public string ModeName { get; init; }
        /// <summary>
        /// if Options is not null.We should configure Persistence by Options or we will get Persistence instance from aspnetcore.
        /// </summary>
        public TOptions? Options { get; private set; }
        protected PersistenceModeConfiguration()
        {
        }

        internal PersistenceModeConfiguration(string modeName)
        {
            ModeName = modeName;
        }

        public PersistenceModeConfiguration<TOptions> WithOptions(TOptions options)
        {
            Options = options;
            return this;
        }
    }
}
