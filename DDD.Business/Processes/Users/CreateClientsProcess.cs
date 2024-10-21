using AutoMapper;
using DDD.Business.Interfaces.Processes.Users;
using DDD.Business.Interfaces.Services.Users;
using DDD.Core.Models.Domain.Users;
using DDD.Core.Models.DTO.Users;

namespace DDD.Business.Processes.Users;

public class CreateClientProcess : BaseProcess, ICreateClientProcess
{
    private readonly IClientsService _clientsService;

    public CreateClientProcess(IMapper mapper, IClientsService clientsService)
        : base(mapper)
    {
        _clientsService = clientsService;
    }

    public async Task<Guid> Process(CreateClientRequest request)
    {
        if (await _clientsService.GetByPhone(request.Phone) != null)
        {
            throw new Exception($"Пользователь с телефоном {request.Phone} уже существует!");
        }

        Client response = await _clientsService.Create(_mapper.Map<Client>(request));

        return response.Id;
    }
}
