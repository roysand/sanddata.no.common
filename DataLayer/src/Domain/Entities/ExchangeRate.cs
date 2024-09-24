namespace DataLayer.Domain.Entities;

public enum ExchangeRateTypes
{
    EUR = 1
}

public class ExchangeRate
{
    public Guid ExchangeRateId { get; set; }

    public DateTime ExchangeRatePeriod { get; set; }

    public decimal? ExchangeRateValue { get; set; }

    public int ExchangeRateType { get; set; }

    public ExchangeRate()
    {
        ExchangeRateId = Guid.NewGuid();
    }
}