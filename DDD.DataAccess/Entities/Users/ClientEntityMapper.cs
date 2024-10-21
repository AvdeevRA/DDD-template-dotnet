using AutoMapper;
using DDD.Core.Models.Domain.Users;

namespace DDD.DataAccess.Entities.Users;

public class ClientEntityMapper : Profile
{
    public ClientEntityMapper()
    {
        CreateMap<ClientEntity, Client>().ReverseMap();
    }
}
