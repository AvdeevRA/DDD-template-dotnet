using AutoMapper;
using DDD.Core.Models.Domain.Users;
using DDD.Core.Models.DTO.Users;

namespace DDD.Core.Maps.Users;

public class ClientMapper : Profile
{
    public ClientMapper()
    {
        CreateMap<CreateClientRequest, Client>();
        CreateMap<Client, GetClientResponse>();
        CreateMap<UpdateClientRequest, Client>();
        CreateMap<Client, GetClientResponse>();
    }
}
