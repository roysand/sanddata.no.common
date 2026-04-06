using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

public enum ExchangeRateTypes
{
    EUR = 1
}

public class ExchangeRate : AuditableEntity
{
    public DateTime ExchangeRatePeriod { get; set; }

    public decimal? ExchangeRateValue { get; set; }

    public int ExchangeRateType { get; set; }

    public ExchangeRate() : base(Guid.NewGuid())
    {
    }

    public ExchangeRate(Guid id) : base(id)
    {
    }
}
