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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DictNode>(entity =>
            {
                entity.ToTable("dictnodes");
                entity.HasKey(e => e.ID);
                entity.HasOne<DDict>()
                      .WithMany(d => d.DictNodes)
                      .HasForeignKey(dn => dn.dictid);
            });

            modelBuilder.Entity<DDict>(entity =>
            {
                entity.ToTable("dicts");
                entity.HasKey(e => e.ID);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

