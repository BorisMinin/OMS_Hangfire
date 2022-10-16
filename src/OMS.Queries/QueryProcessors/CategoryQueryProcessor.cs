using Microsoft.EntityFrameworkCore;
using OMS.API.Models.Dtos.CategoryDto;
using OMS.Data.Access.DAL;
using OMS.Data.Model.Entities;
using OMS.Queries.AppHelpers;
using OMS.Queries.CrossCuttingConcerns;
using OMS.Queries.Interfaces;
using System.Collections.Generic;

namespace OMS.Queries.QueryProcessors
{
    public class CategoryQueryProcessor : ICategoryQueryProcessor
    {
        private IUnitOfWork _unitOfWork;    
        private readonly string cacheKey = $"{typeof(Category)}";
        private readonly static CacheTech cacheTech = CacheTech.Memory;

        private Func<CacheTech, ICacheService> _cacheService;

        public CategoryQueryProcessor(IUnitOfWork unitOfWork, Func<CacheTech, ICacheService> cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public IQueryable<Category> Get()
        {
            return _cacheService(cacheTech).GetCache<Category>(_unitOfWork, cacheKey);
        }

        private IQueryable<Category> GetQuery()
        {
            return _unitOfWork.Query<Category>();
        }

        public Category Get(int id)
        {
            return GetQuery().AsNoTracking().FirstOrDefault(x => x.CategoryId == id);
        }

        public async Task<Category> Create(CategoryDtoCreate dto, CancellationToken token)
        {
            var category = new Category()
            {
                CategoryName = dto.CategoryName,
                Description = dto.Description
            };

            await this._unitOfWork.Add(category, token);
            await this._unitOfWork.CommitAsync(token);

            return category;
        }

        public async Task<Category> Update(int id, CategoryDtoUpdate dto, CancellationToken token)
        {
            var category = GetQuery().FirstOrDefault(x => x.CategoryId == id);

            category.CategoryName = dto.CategoryName;
            category.Description = dto.Description;

            await _unitOfWork.CommitAsync(token);

            return category;
        }

        public async Task Delete(int id, CancellationToken token)
        {
            var category = GetQuery().AsNoTracking().FirstOrDefault(x => x.CategoryId == id);

            _unitOfWork.Delete(category, token);
            await _unitOfWork.CommitAsync(token);
        }
       
        public async Task RefreshCache()
        {
            _cacheService(cacheTech).Remove(cacheKey);
            var cachedList = await _unitOfWork.Query<Category>().ToListAsync();
            _cacheService(cacheTech).Set(cacheKey, cachedList);
        }
    }
}