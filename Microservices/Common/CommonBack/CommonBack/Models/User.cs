using CommonBack.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace CommonBack.Models
{
    public class User : BaseEntity
    {


        [Required] // Not null
        [StringLength(maximumLength: 200)]
        [Column(name: "name")]
        public string Name { get; set; }


        [MaybeNull]
        [Column(name: "phone")]
        public string Phone { get; set; }


        [MaybeNull]
        [Column(name: "email")]
        public string Email { get; set; }

        public ICollection<Chat> Chats { get; set; }
        public User()
        {
            Chats = new List<Chat>();
        }
    }
}
