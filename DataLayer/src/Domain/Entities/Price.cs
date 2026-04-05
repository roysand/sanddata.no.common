using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

public class Price : AuditableEntity
{
    public DateTime PricePeriod { get; set; }
    public DateTime Modified { get; set; }

    public Guid LocationId { get; set; }

    public string Currency { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public decimal? Average { get; set; }

    public decimal? Max { get; set; }

    public decimal Min { get; set; }

    public string InDomain { get; set; } = null!;

    public string OutDomain { get; set; } = null!;

    public Location? Location { get; set; }

    public List<PriceDetail> PriceDetailList { get; private set; }

    public Price() : base(Guid.NewGuid())
    {
        PriceDetailList = new List<PriceDetail>();
    }

    public Price(Guid id, DateTime pricePeriod, Guid locationId, string currency, string unit, decimal average, decimal max, decimal min, string inDomain, string outDomain, List<PriceDetail> priceDetailList)
        : base(id)
    {
        PricePeriod = pricePeriod;
        LocationId = locationId;
        Currency = currency;
        Unit = unit;
        Average = average;
        Max = max;
        Min = min;
        InDomain = inDomain;
        OutDomain = outDomain;
        PriceDetailList = priceDetailList;
    }

    public new string ToString()
    {
        return $"Date: {this.PricePeriod} Unit: {this.Unit} Currency: {this.Currency} Location: {this.Location}";
    }
}
