using FullStackDataStructureVisualizer.Server.Data;
using FullStackDataStructureVisualizer.Server.Controllers;
using FullStackDataStructureVisualizer.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FullStackDataStructureVisualizer.Tests
{
    public class GraphControllerTests
    {
        protected readonly ITestOutputHelper output;

        public GraphControllerTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task TestGetGraphsAsync()
        {
            GraphController gc = new GraphController(CreateGraphDbContext());
            var graph = await gc.Get();

            output.WriteLine("Graph ID: " + graph.Value.ID +
                             "\nIs Directed: " + graph.Value.IsDirected +
                             "\nVertices:\n{");

            foreach(Vertex v in graph.Value.Vertices)
            {
                output.WriteLine("    {\n        ID: " + v.ID +
                                        "\n        Label: " + v.Label +
                                        "\n        Value: " + v.value + 
                                        "\n        x: " + v.X +
                                        "\n        y: " + v.Y +
                                        "\n        graphid: " + v.graphid +
                                 "\n    },");
            }
            output.WriteLine("}\nEdges:\n{");
            foreach(Edge e in graph.Value.Edges) 
            { 
                output.WriteLine("    {\n        ID: " + e.ID +
                                        "\n        From: " + e.From +
                                        "\n        To: " + e.To +
                                        "\n        Weight: " + e.Weight +
                                        "\n        graphid: " + e.graphid +
                                 "\n    },");
            }
            output.WriteLine("}\n");
        }

        private GraphDbContext CreateGraphDbContext()
        {
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var defaultConnection = $"{connectionString}Password={dbPassword}";

            var options = new DbContextOptionsBuilder<GraphDbContext>()
                .UseNpgsql(defaultConnection)
                .Options;

            var dbContext = new GraphDbContext(options);

            return dbContext;
        }
    }
}