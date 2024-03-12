using Microsoft.EntityFrameworkCore;
using FullStackDataStructureVisualizer.Server.Models;

namespace FullStackDataStructureVisualizer.Server.Data
{
    public class AArrayDbContext : DbContext
    {
        public AArrayDbContext(DbContextOptions<AArrayDbContext> options)
        : base(options) { }

        public DbSet<ArrayNode> ArrayNodes { get; set; }

        public DbSet<AArray> AArray { get; set; }
    }
}

