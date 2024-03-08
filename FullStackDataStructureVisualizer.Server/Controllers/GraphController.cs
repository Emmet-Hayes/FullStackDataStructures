using Microsoft.AspNetCore.Mvc;


namespace FullStackDataStructureVisualizer.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GraphController : ControllerBase
    {
        [HttpGet(Name = "Graph")]
        public ActionResult<Graph> Get()
        {
            Console.WriteLine("Does this ever Happen?");
            Graph g = new Graph();
            g.Nodes.Add(new Node(1, 0.0f, 0.0f, "A"));
            g.Nodes.Add(new Node(2, 30.0f, 30.0f, "B"));
            g.Nodes.Add(new Node(3, 60.0f, 60.0f, "C"));
            g.Nodes.Add(new Node(4, -30.0f, -30.0f, "D"));
            g.Nodes.Add(new Node(5, -30.0f, 30.0f, "E"));
            g.Nodes.Add(new Node(6, 30f, -30f, "F"));
            g.Edges.Add(new Edge(1, 2, 2.5f));
            Console.WriteLine("Graph Created: " + g);
            return g;
        }
    }
}
