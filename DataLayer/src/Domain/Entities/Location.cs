namespace DataLayer.Domain.Entities;

public class Location
{
    public Guid LocationId { get; set; }

    public string LocationName { get; set; } = null!;

    public string? LocationAddress { get; set; }

    public string? SerialNumber { get; set; }

    public Guid? ApiKeyId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ChangedDate { get; set; }

    public virtual ApiKey? ApiKey { get; set; }
}