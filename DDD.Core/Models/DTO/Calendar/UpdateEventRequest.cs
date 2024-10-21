using System.ComponentModel.DataAnnotations;

namespace DDD.Core.Models.DTO.Calendar;

/// <summary>
/// Запрос обновления данных мероприятия
/// </summary>
public record UpdateEventRequest()
{
    /// <summary>
    /// ID
    /// </summary>
    [Required]
    public required Guid Id { get; set; }

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
