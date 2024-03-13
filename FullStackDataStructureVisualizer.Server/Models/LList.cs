using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FullStackDataStructureVisualizer.Server.Models
{
    [Table("listnodes")]
    public class ListNode
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

        [Column("listid")]
        public long listid { get; set; }

        [Column("nextid")]
        public long? nextid { get; set; }

        [Column("value")]
        public int value { get; set; }

        // For EF Core
        public ListNode()
        {
        }
    }

    [Table("lists")]
    public class LList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long ID { get; set; }

        [Column("isdoublylinked")]
        public bool isDoublyLinked { get; set; }

        public List<ListNode> ListNodes { get; set; }

    }
}
