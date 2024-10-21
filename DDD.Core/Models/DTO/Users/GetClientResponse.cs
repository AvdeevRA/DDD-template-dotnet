namespace DDD.Core.Models.DTO.Users;

/// <summary>
/// Ответ получения данных клиента
/// </summary>
public record GetClientResponse
{
    /// <summary>
    /// ID
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Дата регистрации
    /// </summary>
    public DateTime? CreatedDate { get; set; }
}
