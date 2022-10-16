using Microsoft.EntityFrameworkCore;
using OMS.Data.Access.DAL;
using OMS.Queries.AppHelpers;

namespace OMS.Queries.CrossCuttingConcerns
{
    public static class CacheService
    {
        public static IQueryable<T> GetCache<T>(this ICacheService _cacheService, IUnitOfWork _unitOfWork, string cacheKey) where T : class
        {
            if (!_cacheService.TryGet(cacheKey, out IReadOnlyList<T> cachedList))
            {
                var query = _unitOfWork.Query<T>().AsNoTracking();

                cachedList = query.ToList();
                _cacheService.Set(cacheKey, cachedList);
                return query;
            }
            return _unitOfWork.Query<T>().AsNoTracking();
        }

        public static async Task RefreshCacheAsync<T>(this ICacheService _cacheService, IUnitOfWork _unitOfWork, string cacheKey) where T : class
        {
            _cacheService.Remove(cacheKey);
            var list = await _unitOfWork.Query<T>().ToListAsync();
            _cacheService.Set(cacheKey, list);
        }
    }
}