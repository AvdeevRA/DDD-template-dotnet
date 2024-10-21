using DDD.Core.Models.DTO.Calendar;

namespace DDD.Business.Interfaces.Processes.Calendar;

public interface IGetEventProcess
{
    public Task<GetEventResponse?> Process(Guid id);
}
