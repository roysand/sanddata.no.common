using DataLayer.Application.Interface.Repositories;

namespace DataLayer.Application.Interface;

public interface ILocationRepository<T> : IBaseRepository<T> where T : class
{
}