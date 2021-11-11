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
        public TOptions Options { get; private set; }
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

        public PersistenceModeConfiguration<TOptions> WithOptions(Action<TOptions> action)
        {
            action(this.Options);
            return this;
        }
    }
}
