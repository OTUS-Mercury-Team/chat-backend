using ACommonAuth.Contracts.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonAuth.Contracts.Request
{
    public record CreateUserModel: LoginModel
    {
        [Required]
        public string Email { get; init; }
    }
}
