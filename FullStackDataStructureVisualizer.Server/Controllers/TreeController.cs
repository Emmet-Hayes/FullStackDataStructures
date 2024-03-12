using FullStackDataStructureVisualizer.Server.Data;
using FullStackDataStructureVisualizer.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FullStackDataStructureVisualizer.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TreeController : ControllerBase
    {
        private readonly TreeDbContext _context;

        public TreeController(TreeDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "Tree")]
        public async Task<ActionResult<Tree>> Get()
        {
            var treeList = await _context.Trees.ToListAsync();
            var tree = treeList.ElementAt(0); // just get 1st element for now
            if (tree == null)
                return NotFound();

            // Assuming Graph has properties to directly assign these collections
            tree.TreeNodes = await _context.TreeNodes.ToListAsync();

            return tree;
        }
    }
}
