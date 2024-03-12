using Microsoft.EntityFrameworkCore;
using FullStackDataStructureVisualizer.Server.Models;

namespace FullStackDataStructureVisualizer.Server.Data
{
    public class LListDbContext : DbContext
    {
        public LListDbContext(DbContextOptions<LListDbContext> options)
        : base(options) { }

        public DbSet<ListNode> ListNodes { get; set; }

        public DbSet<LList> LList { get; set; }
    }
}
