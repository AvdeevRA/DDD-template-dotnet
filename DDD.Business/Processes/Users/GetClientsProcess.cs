using AutoMapper;
using DDD.Business.Interfaces.Processes.Users;
using DDD.Business.Interfaces.Services.Users;
using DDD.Core.Models.Domain.Users;
using DDD.Core.Models.DTO.Users;

namespace DDD.Business.Processes.Users;

public class GetClientsProcess : BaseProcess, IGetClientsProcess
{
    private readonly IClientsService _clientsService;

    public GetClientsProcess(IMapper mapper, IClientsService clientsService)
        : base(mapper)
    {
        _clientsService = clientsService;
    }

    public async Task<List<GetClientResponse>> Process(
        string? fullname = null,
        string? phone = null,
        string? email = null,
        DateTime? birthdate = null
    )
    {
        List<Client> clients = await _clientsService.Get(fullname, phone, email, birthdate);

        if (clients.Count != 0)
        {
            await this.CheckAccess(clients);
        }

        return _mapper.Map<List<GetClientResponse>>(clients);
    }

    private async Task<bool> CheckAccess(List<Client> clients)
    {
        return true;
    }
}
