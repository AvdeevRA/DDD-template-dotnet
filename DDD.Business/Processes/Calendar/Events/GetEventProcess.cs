using AutoMapper;
using DDD.Business.Interfaces.Processes.Calendar;
using DDD.Business.Interfaces.Services.Calendar;
using DDD.Core.Models.Domain.Calendar;
using DDD.Core.Models.DTO.Calendar;

namespace DDD.Business.Processes.Calendar.Events;

public class GetEventProcess : BaseProcess, IGetEventProcess
{
    private readonly IEventsService _eventsService;

    public GetEventProcess(IMapper mapper, IEventsService eventsService)
        : base(mapper)
    {
        _eventsService = eventsService;
    }

    public async Task<GetEventResponse?> Process(Guid id)
    {
        var @event = await _eventsService.Get(id);

        if (@event != null)
        {
            await CheckAccess(@event);
        }

        return @event == null ? null : _mapper.Map<GetEventResponse>(@event);
    }

    private async Task<bool> CheckAccess(Event @event)
    {
        return true;
    }
}
