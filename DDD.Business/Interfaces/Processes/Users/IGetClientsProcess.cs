using DDD.Core.Models.DTO.Users;

namespace DDD.Business.Interfaces.Processes.Users;

public interface IGetClientsProcess
{
    public Task<List<GetClientResponse>> Process(
        string? fullname = null,
        string? phone = null,
        string? email = null,
        DateTime? birthdate = null
    );
}
