using FluentAssertions;
using Moq;
using OMS.API.Models.Dtos.CategoryDto;
using OMS.Data.Access.DAL;
using OMS.Data.Model.Entities;
using OMS.Queries.Interfaces;
using OMS.Queries.QueryProcessors;

namespace OMS.Queries.Tests
{
    public class CategoryQueryProcessorTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private List<Category> _categoryList;
        private ICategoryQueryProcessor _queryProcessor;
        //private Random _random;
        private Category _category;
        private CancellationToken _token;

        public CategoryQueryProcessorTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();

            _categoryList = new List<Category>();
            _unitOfWork.Setup(x => x.Query<Category>()).Returns(() => _categoryList.AsQueryable());
            _category = new Category { CategoryId = 1, CategoryName = "Beverages", Description = "Soft drinks, coffees, teas, beers, and ales" };

            _queryProcessor = new CategoryQueryProcessor(_unitOfWork.Object);
        }

        [Fact]
        public void GetByIdTest()
        {
            var category = new Category { CategoryId = _category.CategoryId };
            _categoryList.Add(category);

            var result = _queryProcessor.Get(category.CategoryId);

            result.Should().Be(category);
        }

        [Fact]
        public async Task CreateCategory()
        {
            var entity = new CategoryDtoCreate
            {
                CategoryName = _category.CategoryName,
                Description = _category.Description,
            };

            var result = await _queryProcessor.Create(entity, _token);

            result.CategoryName.Should().Be(entity.CategoryName);
            result.Description.Should().Be(entity.Description);
            _unitOfWork.Verify(x => x.Add(result, _token));
        }

        [Fact]
        public async Task UpdateCategory()
        {
            var category = new Category { CategoryId = _category.CategoryId };
            _categoryList.Add(category);

            var entity = new CategoryDtoUpdate
            {
                CategoryName = _category.CategoryName,
                Description = _category.Description,
            };

            var result = await _queryProcessor.Update(category.CategoryId, entity, _token);

            result.Should().Be(category);
            result.Description.Should().Be(entity.Description);

        }
    }
}