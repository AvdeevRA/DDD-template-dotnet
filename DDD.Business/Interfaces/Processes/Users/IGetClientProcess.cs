using DDD.Core.Models.DTO.Users;

namespace DDD.Business.Interfaces.Processes.Users;

public interface IGetClientProcess
{
    public Task<GetClientResponse?> Process(Guid id);
}
