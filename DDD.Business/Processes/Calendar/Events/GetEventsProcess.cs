using AutoMapper;
using DDD.Business.Interfaces.Processes.Calendar;
using DDD.Business.Interfaces.Services.Calendar;
using DDD.Core.Models.Domain.Calendar;
using DDD.Core.Models.DTO.Calendar;

namespace DDD.Business.Processes.Calendar.Events;

public class GetEventsProcess : BaseProcess, IGetEventsProcess
{
    private readonly IEventsService _eventsService;

    public GetEventsProcess(IMapper mapper, IEventsService eventsService)
        : base(mapper)
    {
        _eventsService = eventsService;
    }

    public async Task<List<GetEventResponse>> Process(
        DateTime? dateFrom = null,
        DateTime? dateTo = null
    )
    {
        List<Event> @events = await _eventsService.Get(dateFrom, dateTo);

        if (@events.Count != 0)
        {
            await CheckAccess(@events);
        }

        return _mapper.Map<List<GetEventResponse>>(@events);
    }

    private async Task<bool> CheckAccess(List<Event> @event)
    {
        return true;
    }
}
