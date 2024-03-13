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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArrayNode>(entity =>
            {
                entity.ToTable("arraynodes");
                entity.HasKey(e => e.ID);
                entity.HasOne<AArray>()
                      .WithMany(a => a.ArrayNodes)
                      .HasForeignKey(an => an.arrayid);
            });

            modelBuilder.Entity<AArray>(entity =>
            {
                entity.ToTable("arrays");
                entity.HasKey(e => e.ID);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

