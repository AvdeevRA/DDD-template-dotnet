using AutoMapper;
using DDD.Business.Interfaces.Processes.Calendar;
using DDD.Business.Interfaces.Services.Calendar;
using DDD.Business.Services.Calendar;

namespace DDD.Business.Processes.Calendar.Events;

public class DeleteEventProcess : BaseProcess, IDeleteEventProcess
{
    private readonly IEventsService _eventsService;

    public DeleteEventProcess(IMapper mapper, IEventsService eventsService)
        : base(mapper)
    {
        _eventsService = eventsService;
    }

    public async Task Process(Guid id)
    {
        var @event = await _eventsService.Get(id);

        if (@event == null)
        {
            throw new KeyNotFoundException();
        }

        await _eventsService.Delete(@event);

        return;
    }
}
