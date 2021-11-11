using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Infrastructure.Configurations;
using TagS.Infrastructure.Enumerations.Generic;

namespace TagS.Infrastructure.Enumerations
{
    public class PersistenceMode
    {
        private PersistenceMode() { }
        public static PersistenceModeConfiguration<MongoDBConfiguration> MongoDB = new PersistenceModeConfiguration<MongoDBConfiguration>("MongoDB");
        public static PersistenceModeConfiguration<DbContextOptions> SQLDatabase = new PersistenceModeConfiguration<DbContextOptions>("SQLDatabase");
        public static PersistenceModeConfiguration<FilePersistenceConfiguration> File = new PersistenceModeConfiguration<FilePersistenceConfiguration>("File");
    }
}
