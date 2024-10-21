using DDD.Core.Models.DTO.Calendar;

namespace DDD.Business.Interfaces.Processes.Calendar;

public interface IGetEventsProcess
{
    public Task<List<GetEventResponse>> Process(DateTime? dateFrom = null, DateTime? dateTo = null);
}
