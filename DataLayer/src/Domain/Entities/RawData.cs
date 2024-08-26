namespace DataLayer.Domain.Entities;

public class RawData
{
    public Guid MeasurementId { get; set; }

    public DateTime TimeStamp { get; set; }

    public string Location { get; set; } = null!;

    public string Raw1 { get; set; } = null!;

    public bool IsNew { get; set; }

    public RawData()
    {
        IsNew = false;
    }
}