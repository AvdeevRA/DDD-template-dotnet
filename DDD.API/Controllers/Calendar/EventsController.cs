using System.ComponentModel.DataAnnotations;
using DDD.Business.Interfaces.Processes.Calendar;
using DDD.Core.Models.DTO.Calendar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ess.API.Controllers.Calendar;

/// <summary>
/// Контроллер мероприятий
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly ICreateEventProcess _createEventProcess;
    private readonly IGetEventProcess _getEventProcess;
    private readonly IUpdateEventProcess _updateEventProcess;
    private readonly IDeleteEventProcess _deleteEventProcess;
    private readonly IGetEventsProcess _getEventsProcess;

    /// <summary>
    /// </summary>
    /// <param name="createEventProcess"></param>
    /// <param name="getEventProcess"></param>
    /// <param name="updateEventProcess"></param>
    /// <param name="deleteEventProcess"></param>
    /// <param name="getEventsProcess"></param>
    public EventsController(
        ICreateEventProcess createEventProcess,
        IGetEventProcess getEventProcess,
        IUpdateEventProcess updateEventProcess,
        IDeleteEventProcess deleteEventProcess,
        IGetEventsProcess getEventsProcess
    )
    {
        _createEventProcess = createEventProcess;
        _getEventProcess = getEventProcess;
        _updateEventProcess = updateEventProcess;
        _deleteEventProcess = deleteEventProcess;
        _getEventsProcess = getEventsProcess;
    }

    /// <summary>
    /// Метод создания мероприятия
    /// </summary>
    /// <permission>Permission.Calendar.Modify</permission>
    /// <param name="request">Тело данных мероприятия</param>
    /// <response code="201">Мероприятие создано</response>
    /// <response code="403">Ошибка прав</response>
    /// <response code="400">Ошибка создания</response>
    [HttpPost]
    //[Authorize(Permission.Calendar.Modify)]
    [ProducesResponseType(201)]
    [ProducesResponseType(403)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateEventRequest request)
    {
        try
        {
            var eventId = await _createEventProcess.Process(request);

            return Created();
        }
        catch (Exception exception)
        {
            return BadRequest("Произошла ошибка при создании мероприятия!");
        }
    }

    /// <summary>
    /// Метод получения данных мероприятия
    /// </summary>
    /// <permission>Permission.Calendar.Read и пользователи, которые имеют доступ к мероприятию</permission>
    /// <param name="id">Guid мероприятия</param>
    /// <response code="200">Данные мероприятия</response>
    /// <response code="403">Ошибка прав</response>
    /// <response code="404">Мероприятие не найдено</response>
    /// <response code="400">Ошибка получения данных мероприятия</response>
    [HttpGet("{id:guid}")]
    //[Authorize]
    [ProducesResponseType(typeof(GetEventResponse), 200)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<GetEventResponse>> Get([Required] Guid id)
    {
        try
        {
            var @event = await _getEventProcess.Process(id);

            return @event == null ? NotFound() : Ok(@event);
        }
        catch (UnauthorizedAccessException exception)
        {
            return Forbid();
        }
        catch (Exception exception)
        {
            return BadRequest("Произошла ошибка при поиске мероприятия!");
        }
    }

    /// <summary>
    /// Метод получения данных мероприятий
    /// </summary>
    /// <permission>Permission.Calendar.Read и пользователи, которые имеют доступ к мероприятию</permission>
    /// <param name="dateFrom">Фильтр "Даты начала" начало</param>
    /// <param name="dateTo">Фильтр "Даты начала" конец</param>
    /// <param name="eventType">Тип мероприятия</param>
    /// <response code="200">Данные мероприятий</response>
    /// <response code="403">Ошибка прав</response>
    /// <response code="400">Ошибка получения данных мероприятий</response>
    [HttpGet]
    //[Authorize]
    [ProducesResponseType(typeof(List<GetEventResponse>), 200)]
    [ProducesResponseType(403)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<List<GetEventResponse>>> Get(
        DateTime? dateFrom = null,
        DateTime? dateTo = null
    )
    {
        try
        {
            return Ok(await _getEventsProcess.Process(dateFrom, dateTo));
        }
        catch (UnauthorizedAccessException exception)
        {
            return Forbid();
        }
        catch (Exception exception)
        {
            return BadRequest("Произошла ошибка при поиске мероприятий!");
        }
    }

    /// <summary>
    /// Метод обновления данных мероприятия
    /// </summary>
    /// <permission>Permission.Calendar.Modify</permission>
    /// <param name="request">Тело данных мероприятия</param>
    /// <response code="200">Данные мероприятия</response>
    /// <response code="403">Ошибка прав</response>
    /// <response code="400">Ошибка обновления данных мероприятия</response>
    [HttpPut]
    //[Authorize(Permission.Calendar.Modify)]
    [ProducesResponseType(typeof(GetEventResponse), 200)]
    [ProducesResponseType(403)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<GetEventResponse>> Update([FromBody] UpdateEventRequest request)
    {
        try
        {
            var @event = await _updateEventProcess.Process(request);

            return Ok(@event);
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound();
        }
        catch (Exception exception)
        {
            return BadRequest("Произошла ошибка при обновлении мероприятия!");
        }
    }

    /// <summary>
    /// Метод удаления мероприятия
    /// </summary>
    /// <permission>Permission.Calendar.Modify</permission>
    /// <param name="id">Guid мероприятия</param>
    /// <response code="204">Успешное удаление</response>
    /// <response code="403">Ошибка прав</response>
    /// <response code="400">Ошибка удаления мероприятия</response>
    [HttpDelete("{id:guid}")]
    //[Authorize(Permission.Calendar.Modify)]
    [ProducesResponseType(204)]
    [ProducesResponseType(403)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Delete([Required] Guid id)
    {
        try
        {
            await _deleteEventProcess.Process(id);
            return NoContent();
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound();
        }
        catch (Exception exception)
        {
            return BadRequest("Произошла ошибка при удалении мероприятия!");
        }
    }
}
