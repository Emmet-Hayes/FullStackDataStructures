using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FullStackDataStructureVisualizer.Server.Models
{
    [Table("arraynodes")]
    public class ArrayNode
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

        [Column("arrayid")]
        public long arrayid { get; set; }

        [Column("index")]
        public long? index { get; set; }

        [Column("value")]
        public int value { get; set; }

        // For EF Core
        public ArrayNode()
        {
        }

        public ArrayNode(int id, float x, float y, string label)
        {
            ID = id;
            X = x;
            Y = y;
            Label = label;
        }
    }

    [Table("arrays")]
    public class AArray
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long ID { get; set; }

        [Column("isdynamic")]
        public bool isdynamic { get; set; }

        public List<ArrayNode> ArrayNodes { get; set; }

    }
}
