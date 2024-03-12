using FullStackDataStructureVisualizer.Server.Data;
using FullStackDataStructureVisualizer.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FullStackDataStructureVisualizer.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DDictController : ControllerBase
    {
        private readonly DDictDbContext _context;

        public DDictController(DDictDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "Dict")]
        public async Task<ActionResult<DDict>> Get()
        {
            var ddictList = await _context.DDict.ToListAsync();
            var ddict = ddictList.ElementAt(0); // just get 1st element for now
            if (ddict == null)
                return NotFound();

            // Assuming Graph has properties to directly assign these collections
            ddict.DictNodes = await _context.DDictNodes.ToListAsync();

            return ddict;
        }
    }
}
