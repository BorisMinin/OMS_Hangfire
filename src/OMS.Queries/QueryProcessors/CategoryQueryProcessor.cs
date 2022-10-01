using Microsoft.EntityFrameworkCore;
using OMS.API.Models.Dtos.CategoryDto;
using OMS.Data.Access.DAL;
using OMS.Data.Model.Entities;
using OMS.Queries.Interfaces;

namespace OMS.Queries.QueryProcessors
{
    public class CategoryQueryProcessor : ICategoryQueryProcessor
    {
        private IUnitOfWork _unitOfWork;

        public CategoryQueryProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Category> Get()
        {
            return GetQuery();
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
    }
}