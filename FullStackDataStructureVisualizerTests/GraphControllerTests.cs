using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FullStackDataStructureVisualizer.Server.Controllers;
using FullStackDataStructureVisualizer.Server.Models;


namespace FullStackDataStructureVisualizerTests
{
    public class GraphControllerTests
    {
        protected readonly ITestOutputHelper output;

        public GraphControllerTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task TestGetGraph()
        {
            GraphController gc = new GraphController(CreateGraphDbContext());
        }

        [Fact]
        public GraphDbContext CreateGraphDbContext()
        {
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

            // Set up configuration sources
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true) // Adjust the path as needed
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
