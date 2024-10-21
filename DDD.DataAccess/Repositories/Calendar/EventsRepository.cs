using DDD.DataAccess.Entities.Calendar;
using DDD.DataAccess.Interfaces.Calendar;

namespace DDD.DataAccess.Repositories.Calendar;

public class EventsRepository : GenericRepository<EventEntity>, IEventsRepository
{
    public EventsRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }
}
