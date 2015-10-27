using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpYeni.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class CarpmaContext : DbContext
    {
        public CarpmaContext() : base("DefaultConnectionString")
        {

        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<ScoreSetting> ScoreSettings { get; set; }
    }
}
