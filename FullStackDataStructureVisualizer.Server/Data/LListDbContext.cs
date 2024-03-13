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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListNode>(entity =>
            {
                entity.ToTable("listnodes");
                entity.HasKey(e => e.ID);
                entity.HasOne<LList>()
                      .WithMany(l => l.ListNodes)
                      .HasForeignKey(ln => ln.listid);
            });

            modelBuilder.Entity<LList>(entity =>
            {
                entity.ToTable("lists");
                entity.HasKey(e => e.ID);
            });

            base.OnModelCreating(modelBuilder);
        }
    }



}
