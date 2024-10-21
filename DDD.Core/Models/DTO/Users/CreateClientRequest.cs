using System.ComponentModel.DataAnnotations;

namespace DDD.Core.Models.DTO.Users;

/// <summary>
/// Запрос создания клиента
/// </summary>
public record CreateClientRequest
{
    /// <summary>
    /// Номер телефона
    /// </summary>
    [Required]
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
}
