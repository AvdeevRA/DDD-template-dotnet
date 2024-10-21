using DDD.Core.Models.DTO.Users;

namespace DDD.Business.Interfaces.Processes.Users;

public interface IUpdateClientProcess
{
    public Task<GetClientResponse?> Process(UpdateClientRequest request);
}
