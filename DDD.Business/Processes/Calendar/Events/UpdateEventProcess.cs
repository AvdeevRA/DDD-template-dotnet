using AutoMapper;
using DDD.Business.Interfaces.Processes.Calendar;
using DDD.Business.Interfaces.Services.Calendar;
using DDD.Business.Interfaces.Services.Users;
using DDD.Core.Models.Domain.Calendar;
using DDD.Core.Models.Domain.Users;
using DDD.Core.Models.DTO.Calendar;

namespace DDD.Business.Processes.Calendar.Events;

public class UpdateEventProcess : IUpdateEventProcess
{
    private readonly IEventsService _eventsService;
    private readonly IClientsService _clientsService;
    private readonly IMapper _mapper;

    public UpdateEventProcess(
        IEventsService eventsService,
        IClientsService clientsService,
        IMapper mapper
    )
    {
        _eventsService = eventsService;
        _clientsService = clientsService;
        _mapper = mapper;
    }

    public async Task<GetEventResponse?> Process(UpdateEventRequest request)
    {
        List<Client>? clients = null;

        if (request.ClientsId != null)
        {
            clients = request
                .ClientsId.Distinct()
                .Select(async client => await _clientsService.Get(client))
                .Select(
                    (t, index) =>
                        t.Result
                        ?? throw new KeyNotFoundException(
                            $"Клиент {request.ClientsId[index]} не найден"
                        )
                )
                .ToList();
        }

        Event existedEvent =
            await _eventsService.Get(request.Id) ?? throw new KeyNotFoundException();

        var @event = ParseToEvent(request, clients);
        @event.CreatedDate = existedEvent.CreatedDate;

        var response = await _eventsService.Update(@event);

        return _mapper.Map<GetEventResponse>(response);
    }

    private Event ParseToEvent(UpdateEventRequest request, List<Client>? clients = null)
    {
        var @event = _mapper.Map<Event>(request);
        @event.Clients = clients;

        return @event;
    }
}
