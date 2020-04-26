using HowToUseChannels.Serivces.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HowToUseChannels.Serivces
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
