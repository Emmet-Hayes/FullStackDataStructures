using Microsoft.EntityFrameworkCore;
using FullStackDataStructureVisualizer.Server.Models;

namespace FullStackDataStructureVisualizer.Server.Data
{
    public class GraphDbContext : DbContext
    {
        public GraphDbContext(DbContextOptions<GraphDbContext> options)
        : base(options) { }

        public DbSet<Vertex> Vertices { get; set; }
        public DbSet<Edge> Edges { get; set; }
        public DbSet<Graph> Graphs { get; set; }
    }
}
