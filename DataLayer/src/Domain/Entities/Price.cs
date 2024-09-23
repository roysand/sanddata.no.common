using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

    public class Price : AuditableEntityBase
    {
        public Guid PriceId { get; set; }

        public DateTime PricePeriod { get; set; }

        public string Location { get; set; } = null!;

        public string Currency { get; set; } = null!;

        public string Unit { get; set; } = null!;

        public decimal? Average { get; set; }

        public decimal? Max { get; set; }

        public decimal Min { get; set; }

        public string InDomain { get; set; } = null!;

        public string OutDomain { get; set; } = null!;

        public List<PriceDetail> PriceDetailList { get; private set; }

        public Price()
        {
            PriceDetailList = new List<PriceDetail>();
            PriceId = Guid.NewGuid();
        }

        public Price(Guid priceId, DateTime pricePeriod, string location, string currency, string unit, decimal average, decimal max, decimal min, string inDomain, string outDomain, List<PriceDetail> priceDetailList)
        {
            PriceId = priceId;
            PricePeriod = pricePeriod;
            Location = location;
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
            var result =
                $"Date: {this.PricePeriod} Unit: {this.Unit} Currency: {this.Currency} Location: {this.Location}";

            return result;
        }
    }
