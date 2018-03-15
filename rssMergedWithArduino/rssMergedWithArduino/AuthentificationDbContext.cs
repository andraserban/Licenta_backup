using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rssMergedWithArduino
{
    public class AuthentificationDbContext: DbContext 
    {
        public AuthentificationDbContext() : base("AuthentificationDbContext")
        {

        }
        public DbSet<LogInDataBase> LogInDatabases { get; set; }

    }
}
