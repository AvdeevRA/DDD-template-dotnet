namespace DDD.Core.Models.DTO.Calendar;

/// <summary>
/// Ответ получения данных мероприятия
/// </summary>
public record GetEventResponse
{
    /// <summary>
    /// ID
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Дата начала
    /// </summary>
    public required DateTime DateTimeStart { get; set; }

    /// <summary>
    /// Дата окончания
    /// </summary>
    public required DateTime DateTimeEnd { get; set; }

    /// <summary>
    /// Массив привязанных клиентов
    /// </summary>
    public Guid[]? ClientsId { get; set; }
};
