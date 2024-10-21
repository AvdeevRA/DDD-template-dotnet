using AutoMapper;
using DDD.Business.Interfaces.Processes.Users;
using DDD.Business.Interfaces.Services.Users;

namespace DDD.Business.Processes.Users;

public class DeleteClientProcess : BaseProcess, IDeleteClientProcess
{
    private readonly IClientsService _clientsService;

    public DeleteClientProcess(IMapper mapper, IClientsService clientsService)
        : base(mapper)
    {
        _clientsService = clientsService;
    }

    public async Task Process(Guid id)
    {
        var client = await _clientsService.Get(id);

        if (client == null)
        {
            throw new KeyNotFoundException();
        }

        await _clientsService.Delete(client);

        return;
    }
}
