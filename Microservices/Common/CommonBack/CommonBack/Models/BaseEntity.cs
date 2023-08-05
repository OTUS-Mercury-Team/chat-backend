using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CommonBack.Messages
{
    public class BaseEntity
    {
        [Key]
        [Column(name: "id")]
        [JsonIgnore]
        public long Id { get; set; }
    }
}
