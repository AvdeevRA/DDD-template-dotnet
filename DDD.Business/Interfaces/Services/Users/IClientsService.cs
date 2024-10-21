using DDD.Core.Models.Domain.Users;

namespace DDD.Business.Interfaces.Services.Users;

public interface IClientsService
{
    Task<Client> Create(Client client);
    Task<Client?> Get(Guid id);
    Task<List<Client>> Get(
        string? fullname = null,
        string? phone = null,
        string? email = null,
        DateTime? dateCreationFrom = null,
        DateTime? dateCreationTo = null
    );
    Task<Client?> GetByPhone(string phone);
    Task<Client> Update(Client client);
    Task Delete(Client client);
}
