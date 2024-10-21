using DDD.DataAccess.Entities.Users;
using DDD.DataAccess.Interfaces.Users;

namespace DDD.DataAccess.Repositories.Users;

public class ClientsRepository : GenericRepository<ClientEntity>, IClientsRepository
{
    public ClientsRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }
}
