using FullStackDataStructureVisualizer.Server.Data;
using FullStackDataStructureVisualizer.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FullStackDataStructureVisualizer.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GraphController : ControllerBase
    {
        private readonly GraphDbContext _context;

        public GraphController(GraphDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetGraph")]
        public async Task<ActionResult<Graph>> Get()
        {
            var graph = await _context.Graphs.FirstOrDefaultAsync();
            if (graph == null)
            {
                return NotFound();
            }

            // Assuming Graph has properties to directly assign these collections
            graph.Nodes = await _context.Nodes.ToListAsync();
            graph.Edges = await _context.Edges.ToListAsync();

            
            return graph;
        }
    }
}
