using Microsoft.EntityFrameworkCore;
using FullStackDataStructureVisualizer.Server.Models;

namespace FullStackDataStructureVisualizer.Server.Data
{
    public class TreeDbContext : DbContext
    {
        public TreeDbContext(DbContextOptions<TreeDbContext> options)
        : base(options) { }

        public DbSet<TreeNode> TreeNodes { get; set; }

        public DbSet<Tree> Trees { get; set; }
    }
}
