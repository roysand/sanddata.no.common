using DataLayer.Domain.Common.Entities;
using DataLayer.Domain.Common.Primitives;

namespace DataLayer.Domain.Entities;

public sealed class Location : AuditableEntity
{
    public Location(Guid id, bool isActive, string locationName, string? locationAddress, string? serialNumber, Guid? apiKeyId)
        : base(id)
    {
        IsActive = isActive;
        LocationName = locationName;
        LocationAddress = locationAddress;
        SerialNumber = serialNumber;
        ApiKeyId = apiKeyId;
    }

    private Guid LocationId { get; set; }
    public bool IsActive { get; set; }
    public string LocationName { get; set; } = null!;

    public string? LocationAddress { get; set; }

    public string? SerialNumber { get; set; }

    public Guid? ApiKeyId { get; set; }

    public  ApiKey? ApiKey { get; set; }
    public ICollection<Detail> Details { get; set; } = new List<Detail>();
    public ICollection<Minute> Minutes { get; set; } = new List<Minute>();
    public ICollection<Hour> Hours { get; set; } = new List<Hour>();
    public ICollection<Day> Days { get; set; } = new List<Day>();
    public ICollection<AppUserLocation> UserLocations { get; init; } = new List<AppUserLocation>();
}