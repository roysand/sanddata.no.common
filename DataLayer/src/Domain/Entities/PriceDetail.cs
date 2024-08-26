namespace DataLayer.Domain.Entities;

public class PriceDetail
{
    public Guid PriceDetailId { get; set; }

    public Guid PriceId { get; set; }

    public DateTime PricePeriod { get; set; }

    public decimal Amount { get; set; }

    public PriceDetail()
    {
        PriceDetailId = Guid.NewGuid();
    }
}