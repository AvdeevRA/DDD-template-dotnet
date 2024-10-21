using DDD.Core.Models.DTO.Calendar;

namespace DDD.Business.Interfaces.Processes.Calendar;

public interface IUpdateEventProcess
{
    public Task<GetEventResponse?> Process(UpdateEventRequest request);
}
