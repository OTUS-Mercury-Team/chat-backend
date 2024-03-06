using System.ComponentModel.DataAnnotations;

namespace ACommonAuth.Contracts.Request;

public record LoginModel
{
    [Required]
    public string UserName { get; init; }

    [Required]
    public string Password { get; init; }
}
