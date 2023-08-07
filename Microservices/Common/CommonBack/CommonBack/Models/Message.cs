using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CommonBack.Messages
{
    public class Message: BaseEntity
    {
        

        [Required]
        [Column(name: "chatid")]
        public int ChatId { get; set; }

        [Required]
        [Column(name: "dt")]
        public DateTime DT { get; set; }

        [Required]
        [Column(name: "userfrom")]
        public int UserFrom { get; set; }

        [Required]
        [Column(name: "body")]
        public string Body { get; set; } = "";
    }
}