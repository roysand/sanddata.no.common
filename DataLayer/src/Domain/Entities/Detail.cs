namespace DataLayer.Domain.Entities;

public class Detail
{
    public Guid Id { get; set; }

    public Guid MeasurementId { get; set; }

    public DateTime TimeStamp { get; set; }

    public string Location { get; set; } = null!;

    public string? Name { get; set; }

    public string? ObisCode { get; set; }

    public string? Unit { get; set; }

    public string? ValueStr { get; set; }

    public decimal? ValueNum { get; set; }

    public byte? ObisCodeId { get; set; }
}