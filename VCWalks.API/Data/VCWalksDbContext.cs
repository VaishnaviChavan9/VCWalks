using Microsoft.EntityFrameworkCore;
using VCWalks.API.Models.Domain;

namespace VCWalks.API.Data
{
    public class VCWalksDbContext: DbContext
    {
        public VCWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
                
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        
    }
}
