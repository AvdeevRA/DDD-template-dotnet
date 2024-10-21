using AutoMapper;
using DDD.Business.Interfaces.Processes.Users;
using DDD.Business.Interfaces.Services.Users;
using DDD.Core.Models.Domain.Users;
using DDD.Core.Models.DTO.Users;

namespace DDD.Business.Processes.Users;

public class UpdateClientProcess : BaseProcess, IUpdateClientProcess
{
    private readonly IClientsService _clientsService;

    public UpdateClientProcess(IMapper mapper, IClientsService clientsService)
        : base(mapper)
    {
        _clientsService = clientsService;
    }

    public async Task<GetClientResponse?> Process(UpdateClientRequest request)
    {
        Client? existedClient = await _clientsService.GetByPhone(request.Phone);

        if (existedClient != null && existedClient.Id != request.Id)
        {
            throw new Exception($"Пользователь с телефоном {request.Phone} уже существует!");
        }

        existedClient =
            await _clientsService.Get(request.Id)
            ?? throw new Exception($"Пользователь с id {request.Id} не найден!");

        await CheckAccess(existedClient);

        Client client = _mapper.Map<Client>(request);
        client.CreatedDate = existedClient.CreatedDate;

        return _mapper.Map<GetClientResponse>(await _clientsService.Update(client));
    }

    private async Task<bool> CheckAccess(Client? client)
    {
        return true;
    }
}
