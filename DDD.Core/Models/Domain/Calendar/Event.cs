using DDD.Core.Models.Domain.Users;

namespace DDD.Core.Models.Domain.Calendar;

public class Event : BaseDomainModel
{
    public required string Name { get; set; }
    public required DateTime DateTimeStart { get; set; }
    public required DateTime DateTimeEnd { get; set; }
    public IReadOnlyList<Client>? Clients { get; set; }
}
