using DDD.Core.Models.DTO.Calendar;

namespace DDD.Business.Interfaces.Processes.Calendar;

public interface ICreateEventProcess
{
    public Task<Guid> Process(CreateEventRequest request);
}
