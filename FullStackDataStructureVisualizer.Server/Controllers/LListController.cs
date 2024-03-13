using FullStackDataStructureVisualizer.Server.Data;
using FullStackDataStructureVisualizer.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FullStackDataStructureVisualizer.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LListController : ControllerBase
    {
        private readonly LListDbContext _context;

        public LListController(LListDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "List")]
        public async Task<ActionResult<LList>> Get()
        {
            Console.WriteLine("List Get was called.");
            var llistList = await _context.LList.ToListAsync();
            var llist = llistList.ElementAt(0); // just get 1st element for now
            if (llist == null)
                return NotFound();

            // Assuming Graph has properties to directly assign these collections
            llist.ListNodes = await _context.ListNodes
                              .FromSqlRaw("SELECT id, label, x, y, listid, nextid FROM listnodes")
                              .ToListAsync();

            return llist;
        }
    }
}
