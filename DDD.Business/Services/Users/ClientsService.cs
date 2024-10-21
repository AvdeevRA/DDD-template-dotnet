using System.Linq.Expressions;
using AutoMapper;
using DDD.Business.Interfaces.Services.Users;
using DDD.Core.Extensions.Expressions;
using DDD.Core.Models.Domain.Users;
using DDD.DataAccess.Entities.Users;
using DDD.DataAccess.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace DDD.Business.Services.Users;

public class ClientsService : IClientsService
{
    private readonly IClientsRepository _clientsRepository;
    private readonly IMapper _mapper;

    public ClientsService(IClientsRepository clientsRepository, IMapper mapper)
    {
        _clientsRepository = clientsRepository;
        _mapper = mapper;
    }

    public async Task<Client> Create(Client client)
    {
        return _mapper.Map<Client>(
            await _clientsRepository.AddAsync(_mapper.Map<ClientEntity>(client))
        );
    }

    public async Task<Client?> Get(Guid id)
    {
        Expression<Func<ClientEntity, bool>> whereExpression = e => e.Id == id;

        var client = await _clientsRepository.GetAsync(whereExpression);

        return client == null ? null : _mapper.Map<Client>(client);
    }

    public async Task<List<Client>> Get(
        string? fullname = null,
        string? phone = null,
        string? email = null,
        DateTime? dateCreationFrom = null,
        DateTime? dateCreationTo = null
    )
    {
        Expression<Func<ClientEntity, bool>> whereExpression = e => true;

        if (fullname != null)
        {
            whereExpression = whereExpression.And(e =>
                EF.Functions.Like(e.FirstName, $"%{fullname}%")
                || EF.Functions.Like(e.LastName, $"%{fullname}%")
            );
        }

        if (phone != null)
        {
            whereExpression = whereExpression.And(e => EF.Functions.Like(e.Phone, $"%{phone}%"));
        }

        if (email != null)
        {
            whereExpression = whereExpression.And(e => EF.Functions.Like(e.Email, $"%{email}%"));
        }

        if (dateCreationFrom != null)
        {
            whereExpression = whereExpression.And(e =>
                e.CreatedDate >= dateCreationFrom.Value.ToUniversalTime()
            );
        }

        if (dateCreationTo != null)
        {
            whereExpression = whereExpression.And(e =>
                e.CreatedDate <= dateCreationTo.Value.ToUniversalTime()
            );
        }

        return _mapper.Map<List<Client>>(await _clientsRepository.GetListAsync(whereExpression));
    }

    public async Task<Client?> GetByPhone(string phone)
    {
        Expression<Func<ClientEntity, bool>> whereExpression = e => e.Phone == phone;

        var client = await _clientsRepository.GetAsync(whereExpression);

        return client == null ? null : _mapper.Map<Client>(client);
    }

    public async Task<Client> Update(Client client)
    {
        return _mapper.Map<Client>(
            await _clientsRepository.UpdateAsync(_mapper.Map<ClientEntity>(client))
        );
    }

    public async Task Delete(Client client)
    {
        await _clientsRepository.DeleteAsync(_mapper.Map<ClientEntity>(client));
        return;
    }
}
