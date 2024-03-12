using Microsoft.EntityFrameworkCore;
using FullStackDataStructureVisualizer.Server.Models;

namespace FullStackDataStructureVisualizer.Server.Data
{
    public class DDictDbContext : DbContext
    {
        public DDictDbContext(DbContextOptions<DDictDbContext> options)
        : base(options) { }

        public DbSet<DictNode> DDictNodes { get; set; }

        public DbSet<DDict> DDict { get; set; }
    }
}

