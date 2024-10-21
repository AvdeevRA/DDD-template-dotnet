using System.ComponentModel.DataAnnotations;

namespace DDD.Core.Models.Domain.Users;

public class Client : BaseDomainModel
{
    [Required]
    public required string Phone { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
