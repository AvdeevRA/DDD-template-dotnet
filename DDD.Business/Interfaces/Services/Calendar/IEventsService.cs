using DDD.Core.Models.Domain.Calendar;

namespace DDD.Business.Interfaces.Services.Calendar;

public interface IEventsService
{
    public Task<Event> Create(Event element);
    public Task<Event?> Get(Guid id);
    public Task<List<Event>> Get(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        Guid? clientId = null
    );
    public Task<Event> Update(Event @event);
    public Task Delete(Event @event);
}
