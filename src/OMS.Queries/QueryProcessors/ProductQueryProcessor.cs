using Hangfire;
using Microsoft.EntityFrameworkCore;
using OMS.API.Models.Dtos.ProductDto;
using OMS.Data.Access.DAL;
using OMS.Data.Model.Entities;
using OMS.Queries.AppHelpers;
using OMS.Queries.CrossCuttingConcerns;
using OMS.Queries.Interfaces;

namespace OMS.Queries.QueryProcessors
{
    public class ProductQueryProcessor : IProductQueryProcessor
    {
        private IUnitOfWork _unitOfWork;
        private readonly string cacheKey = $"{typeof(Category)}";
        private readonly static CacheTech cacheTech = CacheTech.Memory;

        private Func<CacheTech, ICacheService> _cacheService;

        public ProductQueryProcessor(IUnitOfWork unitOfWork, Func<CacheTech, ICacheService> cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }
        
        public IQueryable<Product> Get()
        {
            return _cacheService(cacheTech).GetCache<Product>(_unitOfWork, cacheKey);
        }

        private IQueryable<Product> GetQuery()
        {
            return _unitOfWork.Query<Product>();
        }

        public Product Get(int id)
        {
            return GetQuery().AsNoTracking().FirstOrDefault(x => x.ProductId == id);
        }

        public async Task<Product> Create(ProductDtoCreate dto, CancellationToken token)
        {
            var product = new Product()
            {
                CategoryId = dto.CategoryId,
                ProductName = dto.ProductName,
                UnitPrice = dto.UnitPrice,
                QuantityPerUnit = dto.QuantityPerUnit,
                Discontinued = dto.Discontinued,
                UnitsInStock = dto.UnitsInStock,
                UnitsOnOrder = dto.UnitsOnOrder,
                ReorderLevel = dto.ReorderLevel
            };

            await this._unitOfWork.Add(product, token);
            await this._unitOfWork.CommitAsync(token);

            BackgroundJob.Enqueue(() => _cacheService(cacheTech).RefreshCacheAsync<Product>(_unitOfWork, cacheKey));

            return product;
        }

        public async Task<Product> Update(int id, ProductDtoUpdate dto, CancellationToken token)
        {
            var product = GetQuery().FirstOrDefault(c => c.ProductId == id);

            product.QuantityPerUnit = dto.QuantityPerUnit;
            product.UnitPrice = dto.UnitPrice;
            product.UnitsInStock = dto.UnitsInStock;
            product.UnitsOnOrder = dto.UnitsOnOrder;
            product.ReorderLevel = dto.ReorderLevel;
            product.Discontinued = dto.Discontinued;

            await _unitOfWork.CommitAsync(token);

            BackgroundJob.Enqueue(() => _cacheService(cacheTech).RefreshCacheAsync<Product>(_unitOfWork, cacheKey));

            return product;
        }

        public async Task Delete(int id, CancellationToken token)
        {
            var product = await this._unitOfWork.Query<Product>().FirstOrDefaultAsync(p => p.ProductId == id, token);

            _unitOfWork.Delete(product, token);
            await _unitOfWork.CommitAsync(token);

            BackgroundJob.Enqueue(() => _cacheService(cacheTech).RefreshCacheAsync<Category>(_unitOfWork, cacheKey));
        }
    }
}