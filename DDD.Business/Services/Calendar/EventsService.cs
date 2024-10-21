using System.Linq.Expressions;
using AutoMapper;
using DDD.Business.Interfaces.Services.Calendar;
using DDD.Core.Extensions.Expressions;
using DDD.Core.Models.Domain.Calendar;
using DDD.Core.Models.Domain.Users;
using DDD.DataAccess.Entities.Calendar;
using DDD.DataAccess.Interfaces.Calendar;

namespace DDD.Business.Services.Calendar;

public class EventsService : IEventsService
{
    private readonly IEventsRepository _eventsRepository;
    private readonly IMapper _mapper;
    private readonly IEventsClientsRepository _eventsClientsRepository;

    public EventsService(
        IEventsRepository eventsRepository,
        IMapper mapper,
        IEventsClientsRepository eventsClientsRepository
    )
    {
        _eventsRepository = eventsRepository;
        _mapper = mapper;
        _eventsClientsRepository = eventsClientsRepository;
    }

    public async Task<Event> Create(Event @event)
    {
        var clients = @event.Clients;
        @event.Clients = new List<Client>();

        var addedElement = await _eventsRepository.AddAsync(_mapper.Map<EventEntity>(@event));

        if (addedElement != null && clients?.Count > 0)
        {
            await _eventsClientsRepository.AddRangeAsync(
                clients
                    .Select(c => new EventClientEntity()
                    {
                        EventId = addedElement.Id,
                        ClientId = c.Id
                    })
                    .ToList()
            );
        }

        return _mapper.Map<Event>(addedElement);
    }

    public async Task<Event?> Get(Guid id)
    {
        Expression<Func<EventEntity, bool>> whereExpression = e => e.Id == id;
        Expression<Func<EventEntity, object>>[] includeExpression = [e => e.Clients];

        var @event = await _eventsRepository.GetAsync(whereExpression, includeExpression);

        return @event == null ? null : _mapper.Map<Event>(@event);
    }

    public async Task<List<Event>> Get(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        Guid? clientId = null
    )
    {
        Expression<Func<EventEntity, bool>> whereExpression = e => true;
        Expression<Func<EventEntity, object>>[] includeExpression = [e => e.Clients];

        if (clientId != null)
        {
            whereExpression = whereExpression.And(e => e.Clients.Any(c => c.Id == clientId));
            includeExpression = [e => e.Clients.Where(c => c.Id == clientId)];
        }

        if (dateFrom != null)
        {
            whereExpression = whereExpression.And(e => e.DateTimeStart >= dateFrom);
        }

        if (dateTo != null)
        {
            whereExpression = whereExpression.And(e => e.DateTimeStart <= dateTo);
        }

        return _mapper.Map<List<Event>>(
            await _eventsRepository.GetListAsync(whereExpression, includeExpression)
        );
    }

    public async Task<Event> Update(Event @event)
    {
        var clients = @event.Clients;

        @event.Clients = new List<Client>();

        var updatedElement = await _eventsRepository.UpdateAsync(_mapper.Map<EventEntity>(@event));

        var existedRelation = await _eventsClientsRepository.GetListAsync(
            e => e.EventId == updatedElement.Id,
            null,
            false
        );

        if (existedRelation.Count() > 0)
        {
            await _eventsClientsRepository.DeleteRangeAsync(existedRelation);
        }

        if (clients?.Count() > 0)
        {
            await _eventsClientsRepository.AddRangeAsync(
                clients
                    .Select(c => new EventClientEntity()
                    {
                        EventId = updatedElement.Id,
                        ClientId = c.Id
                    })
                    .ToList()
            );
        }

        return @event;
    }

    public async Task Delete(Event @event)
    {
        await _eventsRepository.DeleteAsync(_mapper.Map<EventEntity>(@event));
        return;
    }
}
