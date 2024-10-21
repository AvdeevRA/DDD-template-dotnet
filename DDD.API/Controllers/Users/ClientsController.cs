using DDD.Business.Interfaces.Processes.Users;
using DDD.Core.Models.DTO.Users;
using Microsoft.AspNetCore.Mvc;

namespace DDD.API.Controllers.Users;

/// <summary>
/// Контроллер мероприятий
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ICreateClientProcess _createClientProcess;
    private readonly IGetClientProcess _getClientProcess;
    private readonly IGetClientsProcess _getClientsProcess;
    private readonly IUpdateClientProcess _updateClientProcess;
    private readonly IDeleteClientProcess _deleteClientProcess;

    public ClientsController(
        ICreateClientProcess createClientProcess,
        IGetClientProcess getClientProcess,
        IGetClientsProcess getClientsProcess,
        IUpdateClientProcess updateClientProcess,
        IDeleteClientProcess deleteClientProcess
    )
    {
        _createClientProcess = createClientProcess;
        _getClientProcess = getClientProcess;
        _getClientsProcess = getClientsProcess;
        _updateClientProcess = updateClientProcess;
        _deleteClientProcess = deleteClientProcess;
    }

    /// <summary>
    /// Метод создания клиента
    /// </summary>
    /// <param name="request">Тело данных пользователя</param>
    /// <response code="200">Id созданного клиента</response>
    /// <response code="403">Ошибка прав</response>
    /// <response code="404">Клиент не найден в мессенджерах</response>
    /// <response code="400">Ошибка создания</response>
    [HttpPost]
    //[Authorize(Permission.Client.Modify)]
    [ProducesResponseType(200)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Guid>> Add([FromBody] CreateClientRequest request)
    {
        try
        {
            var clientId = await _createClientProcess.Process(request);

            return Created();
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound($"Ошибка при создании пользователя: {exception.Message}");
        }
        catch (Exception exception)
        {
            return BadRequest($"Ошибка при создании пользователя: {exception.Message}");
        }
    }

    /// <summary>
    /// Метод получения данных клиента
    /// </summary>
    /// <param name="id">Guid пользователя</param>
    /// <response code="200">Данные клиента</response>
    /// <response code="404">Клиент не найден</response>
    /// <response code="403">Ошибка прав</response>
    /// <response code="400">Ошибка поиска клиента</response>
    [HttpGet("{id:guid}")]
    //[Authorize]
    [ProducesResponseType(typeof(GetClientResponse), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<GetClientResponse>> Get(Guid id)
    {
        try
        {
            var client = await _getClientProcess.Process(id);

            return client == null ? NotFound() : Ok(client);
        }
        catch (UnauthorizedAccessException exception)
        {
            return Forbid();
        }
        catch (Exception exception)
        {
            return BadRequest($"Ошибка при поиске пользователя: {exception.Message}");
        }
    }

    /// <summary>
    /// Метод получения данных клиентов
    /// </summary>
    /// <param name="fullname">Имя и фамилия</param>
    /// <param name="phone">Номер телефона</param>
    /// <param name="email">Email</param>
    /// <param name="birthdate">Дата рождения</param>
    /// <response code="200">Данные клиентов</response>
    /// <response code="403">Ошибка прав</response>
    /// <response code="400">Ошибка поиска клиентов</response>
    [HttpGet]
    //[Authorize(Permission.Client.Read)]
    [ProducesResponseType(typeof(List<GetClientResponse>), 200)]
    [ProducesResponseType(403)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<List<GetClientResponse>>> Get(
        string? fullname = null,
        string? phone = null,
        string? email = null,
        DateTime? birthdate = null
    )
    {
        try
        {
            return Ok(await _getClientsProcess.Process(fullname, phone, email, birthdate));
        }
        catch (Exception exception)
        {
            return BadRequest($"Ошибка при поиске пользователей: {exception.Message}");
        }
    }

    /// <summary>
    /// Метод обновления данных клиента
    /// </summary>
    /// <param name="request">Тело данных пользователя</param>
    /// <response code="200">Данные клиента</response>
    /// <response code="403">Ошибка прав</response>
    /// <response code="404">Клиент не найден</response>
    /// <response code="400">Ошибка обновления клиента</response>
    [HttpPut]
    //[Authorize]
    [ProducesResponseType(typeof(GetClientResponse), 200)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<GetClientResponse>> Update(
        [FromBody] UpdateClientRequest request
    )
    {
        try
        {
            return Ok(await _updateClientProcess.Process(request));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (Exception exception)
        {
            return BadRequest($"Ошибка обновления данных пользователя: {exception.Message}");
        }
    }

    /// <summary>
    /// Метод удаления клиента
    /// </summary>
    /// <param name="id">Guid пользователя</param>
    /// <response code="204">Успешное удаление</response>
    /// <response code="403">Ошибка прав</response>
    /// <response code="404">Клиент не найден</response>
    /// <response code="400">Ошибка удаления клиента</response>
    [HttpDelete("{id:guid}")]
    //[Authorize(Permission.Client.Modify)]
    [ProducesResponseType(204)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteClientProcess.Process(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception exception)
        {
            return BadRequest($"Ошибка удаления пользователя: {exception.Message}");
        }
    }
}
