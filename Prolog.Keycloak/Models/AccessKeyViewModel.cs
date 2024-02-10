using System.ComponentModel.DataAnnotations;

namespace Prolog.Keycloak.Models;

public class AccessKeyViewModel
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Required]
    public required string ClientId { get; set; }

    /// <summary>
    /// Secret
    /// </summary>
    [Required]
    public required string ClientSecret { get; set; }
}