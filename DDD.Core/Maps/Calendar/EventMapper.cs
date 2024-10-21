using AutoMapper;
using DDD.Core.Models.Domain.Calendar;
using DDD.Core.Models.DTO.Calendar;

namespace DDD.Core.Maps.Calendar;

public class EventMapper : Profile
{
    public EventMapper()
    {
        CreateMap<CreateEventRequest, Event>();
        CreateMap<Event, GetEventResponse>()
            .ForMember(
                dest => dest.ClientsId,
                opt =>
                    opt.MapFrom(src =>
                        src.Clients == null ? null : src.Clients.Select(c => c.Id).ToArray()
                    )
            );
        CreateMap<UpdateEventRequest, Event>();
        CreateMap<Event, GetEventResponse>()
            .ForMember(
                dest => dest.ClientsId,
                opt =>
                    opt.MapFrom(src =>
                        src.Clients == null ? null : src.Clients.Select(c => c.Id).ToArray()
                    )
            );
        ;
    }
}
