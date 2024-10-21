using AutoMapper;
using DDD.Business.Interfaces.Processes.Calendar;
using DDD.Business.Interfaces.Services.Calendar;
using DDD.Business.Interfaces.Services.Users;
using DDD.Core.Models.Domain.Calendar;
using DDD.Core.Models.Domain.Users;
using DDD.Core.Models.DTO.Calendar;

namespace DDD.Business.Processes.Calendar.Events;

public class CreateEventProcess : BaseProcess, ICreateEventProcess
{
    private readonly IClientsService _clientsService;
    private readonly IEventsService _eventsService;

    public CreateEventProcess(
        IClientsService clientsService,
        IEventsService eventsService,
        IMapper mapper
    )
        : base(mapper)
    {
        _clientsService = clientsService;
        _eventsService = eventsService;
    }

    public async Task<Guid> Process(CreateEventRequest request)
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

        var @event = ParseToEvent(request, clients);

        var response = await _eventsService.Create(@event);

        return response.Id;
    }

    private Event ParseToEvent(CreateEventRequest request, List<Client>? clients)
    {
        var @event = _mapper.Map<Event>(request);
        @event.Clients = clients;

        return @event;
    }
}
