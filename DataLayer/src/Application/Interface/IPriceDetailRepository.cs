using DataLayer.Domain.Entities;
using sanddata.no.ams.api.Application.Common.Interfaces.Repositories;

namespace DataLayer.Application.Interface;

public interface IPriceDetailRepository<T> : IEFRepository<T> where T: class
{
    Task<List<PriceDetail>> GetPricesForCurrentDayAsync(DateOnly date, CancellationToken cancellationToken = default);
    Task<List<PriceDetail>> GetPricesForHoursAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);
}