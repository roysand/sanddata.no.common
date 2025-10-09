using sanddata.no.ams.api.Application.Common.Interfaces.Repositories;

namespace DataLayer.Application.Interface;

public interface IPowerUsageRepository<T> : IEFRepository<T> where T: class
{
    Task GetPowerUsageHourForPeriodAndLocation(DateTime startDate, DateTime endDate, string? location);
}