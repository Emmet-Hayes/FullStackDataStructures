using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackDataStructureVisualizer.Server.Models
{
    [Table("treenodes")]
    public class TreeNode
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

        [Column("treeid")]
        public long treeid { get; set; }

        [Column("parentid")]
        public long? parentid { get; set; }

        [Column("value")]
        public int value { get; set; }

        // For EF Core
        public TreeNode()
        {
        }

        public TreeNode(int id, float x, float y, string label)
        {
            ID = id;
            X = x;
            Y = y;
            Label = label;
        }
    }

    [Table("trees")]
    public class Tree
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long ID { get; set; }

        [Column("isbst")]
        public bool IsBst { get; set; }

        [Column("isbalanced")]
        public bool IsBalanced { get; set; }

        public static int Counter = 0;

        public List<TreeNode> TreeNodes { get; set; }

        public Tree()
        {
            TreeNodes = new List<TreeNode>();
        }

        public Tree(bool isBst = false)
        {
            ID = ++Counter;
            IsBst = isBst;
            TreeNodes = new List<TreeNode>();
        }
    }
}
