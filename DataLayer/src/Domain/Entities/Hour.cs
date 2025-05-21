using System.Security.AccessControl;

namespace DataLayer.Domain.Entities;

public class Hour
{
    public DateTime TimeStamp { get; set; }

    public Guid LocationId { get; set; }
    public string? Unit { get; set; }

    public decimal? ValueNum { get; set; }

    public short? Count { get; set; }
    public Location? Location { get; set; }
}