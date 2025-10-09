using sanddata.no.ams.api.Application.Common.Interfaces.Repositories;

namespace DataLayer.Application.Interface;

public interface ILocationRepository<T> : IEFRepository<T> where T : class
{
}