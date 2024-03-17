namespace Prolog.Application.Clients.Dtos;

/// <summary>
/// Модель добавления клиента
/// </summary>
public class CreateCustomerModel
{
    /// <summary>
    /// Наименование
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string PhoneNumber { get; set; }
}