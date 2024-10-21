using System.ComponentModel.DataAnnotations;
using DDD.DataAccess.Entities.Calendar;

namespace DDD.DataAccess.Entities.Users;

public class ClientEntity : BaseEntity
{
    [Required]
    public string Phone { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<EventEntity>? Events { get; set; }
}
