﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackDataStructureVisualizer.Server.Models
{
    [Table("nodes")]
    public class Node
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long ID { get; set; }
        
        [Column("x")]
        public float X { get; set; }
        
        [Column("y")]
        public float Y { get; set; }
        
        [Column("label")]
        public string? Label { get; set; }

        [Column("graphid")]
        public long graphid { get; set; }

        // For EF Core
        public Node()
        {

        }

        public Node(int id, float x, float y, string label)
        {
            ID = id;
            X = x;
            Y = y;
            Label = label;
        }
    }

    [Table("edges")]
    public class Edge
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        
        [Column("fromnode")]
        public int From { get; set; }

        [Column("tonode")]
        public int To { get; set; }

        [Column("weight")]
        public float Weight { get; set; } = 1;

        [Column("graphid")]
        public long graphid { get; set; }

        public Edge(int from, int to, float weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }

    [Table("graphs")]
    public class Graph
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long ID { get; set; }
        
        [Column("isdirected")]
        public bool IsDirected { get; set; }

        public static int Counter = 0;

        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }

        public Graph()
        {
            Nodes = new List<Node>();
            Edges = new List<Edge>();
        }

        public Graph(bool isDirected = false)
        {
            ID = ++Counter;
            IsDirected = isDirected;
            Nodes = new List<Node>();
            Edges = new List<Edge>();
        }
    }
}