using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests;

public abstract class Request
{
    [Required(ErrorMessage = "User id inválido")]
    public string UserId { get; set; } = string.Empty;
}
