namespace DataLayer.Domain.Entities;

public class Minute
{
    public DateTime TimeStamp { get; set; }

    public string Location { get; set; } = null!;

    public string? Unit { get; set; }

    public decimal? ValueNum { get; set; }

    public short? Count { get; set; }
}