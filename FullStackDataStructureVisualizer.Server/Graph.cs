namespace FullStackDataStructureVisualizer.Server
{
	public class Node
	{
	    public int Id { get; set; }
	    public float X { get; set; }
	    public float Y { get; set; }
	    public string? Label { get; set; }

		public Node(int id, float x, float y, string label) 
		{
			Id = id;
			X = x;
			Y = y;
			Label = label;
		}
	}

	public class Edge
	{
		public int From { get; set; }
		public int To { get; set; }
		public float Weight { get; set; } = 1;

		public Edge(int from, int to, float weight) 
		{
			From = from;
			To = to;
			Weight = weight;
		}
	}

    public class Graph
    {
    	public List<Node> Nodes { get; set; }
    	public List<Edge> Edges { get; set; }

    	public Graph()
    	{
    		Nodes = new List<Node>();
    		Edges = new List<Edge>();
    	}
    }
}
