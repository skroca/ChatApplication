using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Message
    {
        [Key]
        public int IdMessage { get; set; }
        [Column("Message")]
        public string MessageText { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }

        [ForeignKey("IdRoom")]
        public int IdRoom { get; set; }
    }
}
