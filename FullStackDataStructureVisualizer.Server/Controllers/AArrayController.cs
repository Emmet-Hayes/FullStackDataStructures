using FullStackDataStructureVisualizer.Server.Data;
using FullStackDataStructureVisualizer.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FullStackDataStructureVisualizer.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AArrayController : ControllerBase
    {
        private readonly AArrayDbContext _context;

        public AArrayController(AArrayDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "Array")]
        public async Task<ActionResult<AArray>> Get()
        {
            var aarrayList = await _context.AArray.ToListAsync();
            var aarray = aarrayList.ElementAt(0); // just get 1st element for now
            if (aarray == null)
                return NotFound();

            // Assuming Graph has properties to directly assign these collections
            aarray.ArrayNodes = await _context.ArrayNodes.ToListAsync();

            return aarray;
        }
    }
}
