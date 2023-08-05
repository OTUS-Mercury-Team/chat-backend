using CommonBack.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CommonBack.Models
{
    public class Chat : BaseEntity
    {

        [Required] // Not null
        [StringLength(maximumLength: 200)]
        [Column(name: "name")]
        public string Name { get; set; }


        [Required]
        [Column(name: "datecre")]
        public DateTime DateCre { get; set; }

        public ICollection<User> Users { get; set; }
        public Chat()
        {
            Users = new List<User>();
        }
    }
}
