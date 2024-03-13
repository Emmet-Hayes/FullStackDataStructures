using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FullStackDataStructureVisualizer.Server.Models
{
    [Table("dictnodes")]
    public class DictNode
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

        [Column("dictid")]
        public long dictid { get; set; }

        [Column("key")]
        public string key { get; set; }

        [Column("value")]
        public string value { get; set; }

        // For EF Core
        public DictNode()
        {
        }

        public DictNode(int id, float x, float y, string label)
        {
            ID = id;
            X = x;
            Y = y;
            Label = label;
        }
    }

    [Table("dicts")]
    public class DDict
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long ID { get; set; }

        [Column("hashfunction")]
        public string hashfunction { get; set; }

        public List<DictNode> DictNodes { get; set; }

    }
}
