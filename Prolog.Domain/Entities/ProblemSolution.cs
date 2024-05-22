using Prolog.Domain.Abstractions;
using Prolog.Domain.Enums;

namespace Prolog.Domain.Entities;

public class ProblemSolution: BaseEntity<Guid>, IHasTrackDateAttribute
{
    public Guid ProblemId { get; set; }

    /// <summary>
    /// Идентификатор точки доставки
    /// </summary>
    public string LocationId { get; set; } = string.Empty;

    public Guid VehicleId { get; set; }

    public StopTypeEnum StopType { get; set; }

    public int Index { get; set; }

    /// <summary>
    /// Долгота 
    /// </summary>
    public string Longitude { get; set; } = string.Empty;

    /// <summary>
    /// Широта
    /// </summary>
    public string Latitude { get; set; } = string.Empty;

    public DateTimeOffset DateModified { get; set; }
    public DateTimeOffset DateCreated { get; set; }
}