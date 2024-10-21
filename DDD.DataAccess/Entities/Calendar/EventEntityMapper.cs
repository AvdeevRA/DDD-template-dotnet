using AutoMapper;
using DDD.Core.Models.Domain.Calendar;

namespace DDD.DataAccess.Entities.Calendar;

public class EventEntityMapper : Profile
{
    public EventEntityMapper()
    {
        CreateMap<EventEntity, Event>().ReverseMap();
    }
}
