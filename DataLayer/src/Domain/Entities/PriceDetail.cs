using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

public class PriceDetail : AuditableEntity
{
    public Guid PriceId { get; set; }

    public DateTime PricePeriod { get; set; }

    public decimal Amount { get; set; }

    public PriceDetail() : base(Guid.NewGuid())
    {
    }

    public PriceDetail(Guid id) : base(id)
    {
    }
}
