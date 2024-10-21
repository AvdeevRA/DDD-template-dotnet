using AutoMapper;
using DDD.Business.Interfaces.Processes.Users;
using DDD.Business.Interfaces.Services.Users;
using DDD.Core.Models.Domain.Users;
using DDD.Core.Models.DTO.Users;

namespace DDD.Business.Processes.Users;

public class GetClientProcess : BaseProcess, IGetClientProcess
{
    private readonly IClientsService _clientsService;

    public GetClientProcess(IMapper mapper, IClientsService clientsService)
        : base(mapper)
    {
        _clientsService = clientsService;
    }

    public async Task<GetClientResponse?> Process(Guid id)
    {
        var client = await _clientsService.Get(id);

        if (client != null)
        {
            await CheckAccess(client);
        }

        return client == null ? null : _mapper.Map<GetClientResponse>(client);
    }

    private async Task<bool> CheckAccess(Client client)
    {
        return true;
    }
}
