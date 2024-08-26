namespace DataLayer.Domain.Entities;

public class Day
{
    public DateTime Date { get; set; }

    public string Location { get; set; } = null!;

    public string? Unit { get; set; }

    public decimal? ValueNum { get; set; }

    public short? Count { get; set; }

    public decimal? PriceNok { get; set; }
}