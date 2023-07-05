using System.Linq;

namespace WindPowerPlatformAPI.Infrastructure.Helpers.Interfaces
{
    public interface ISortHelper<T>
    {
        IQueryable<T> ApplySort(IQueryable<T> entities, string orderByQueryString);
    }
}
