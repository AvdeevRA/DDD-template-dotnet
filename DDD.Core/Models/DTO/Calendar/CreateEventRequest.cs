using System.ComponentModel.DataAnnotations;

namespace DDD.Core.Models.DTO.Calendar;

/// <summary>
/// Запрос создания мероприятия
/// </summary>
public record CreateEventRequest()
{
    /// <summary>
    /// Название
    /// </summary>
    [Required]
    public required string Name { get; set; }

    /// <summary>
    /// Дата начала
    /// </summary>
    [Required]
    public required DateTime DateTimeStart { get; set; }

    /// <summary>
    /// Дата окончания
    /// </summary>
    [Required]
    public required DateTime DateTimeEnd { get; set; }

    /// <summary>
    /// Массив привязанных клиентов
    /// </summary>
    public Guid[]? ClientsId { get; set; }
};
