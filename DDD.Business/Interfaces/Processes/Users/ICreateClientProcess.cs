using DDD.Core.Models.DTO.Users;

namespace DDD.Business.Interfaces.Processes.Users;

public interface ICreateClientProcess
{
    public Task<Guid> Process(CreateClientRequest request);
}
