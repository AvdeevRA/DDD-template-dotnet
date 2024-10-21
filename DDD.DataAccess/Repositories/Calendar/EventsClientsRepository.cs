using DDD.DataAccess.Entities.Calendar;
using DDD.DataAccess.Interfaces.Calendar;

namespace DDD.DataAccess.Repositories.Calendar;

public class EventsClientsRepository
    : GenericRepository<EventClientEntity>,
        IEventsClientsRepository
{
    public EventsClientsRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }
}
