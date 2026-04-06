using DataLayer.Domain.Common.Entities;
using DataLayer.Domain.Common.Enum;

namespace DataLayer.Domain.Entities;

public class Detail : AuditableEntity
{
    public Guid MeasurementId { get; set; }

    public DateTime TimeStamp { get; set; }

    public Guid LocationId { get; set; }

    public string? Name { get; set; }

    public ObisCodeId ObisCodeId { get; set; }

    public string? ObisCode { get; set; }

    public string? Unit { get; set; }

    public string? ValueStr { get; set; }

    public decimal ValueNum { get; set; }

    public Location? Location { get; set; }

    public Detail(Guid id) : base(id)
    {
        ObisCodeId = ObisCodeId.PowerUsed;
    }

    public Detail() : base(Guid.NewGuid())
    {
        ObisCodeId = ObisCodeId.PowerUsed;
    }
}
