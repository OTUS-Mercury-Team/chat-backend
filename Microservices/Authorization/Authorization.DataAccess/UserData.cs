using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    public class UserData 
    {
        [Key]
        [Column(name: "id")]
        public uint Id { get; set; }

        [MaybeNullAttribute]
        [StringLength(maximumLength: 8)]
        [Column(name: "username")]
        public string Username { get; set; } //   логин

        [MaybeNullAttribute]
        [StringLength(maximumLength: 16)]
        [Column(name: "password")]
        public string Password { get; set; } //   логин

    }
}