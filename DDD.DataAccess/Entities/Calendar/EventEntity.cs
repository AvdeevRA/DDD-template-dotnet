using System.ComponentModel.DataAnnotations;
using DDD.DataAccess.Entities.Users;

namespace DDD.DataAccess.Entities.Calendar;

public class EventEntity : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public DateTime DateTimeStart { get; set; }

    [Required]
    public DateTime DateTimeEnd { get; set; }

    public ICollection<ClientEntity>? Clients { get; set; }
}
