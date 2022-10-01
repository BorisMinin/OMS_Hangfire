using FluentAssertions;
using Moq;
using OMS.API.Models.Dtos.ProductDto;
using OMS.Data.Access.DAL;
using OMS.Data.Model.Entities;
using OMS.Queries.Interfaces;
using OMS.Queries.QueryProcessors;

namespace OMS.Queries.Tests
{
    public class ProductQueryProcessorTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private List<Product> _productList;
        private IProductQueryProcessor _queryProcessor;
        private Product _product;
        private CancellationToken _token;

        public ProductQueryProcessorTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();

            _productList = new List<Product>();
            _unitOfWork.Setup(x => x.Query<Product>()).Returns(() => _productList.AsQueryable());
            _product = new Product { ProductId = 1, ProductName = "Chai", CategoryId = 1};

            _queryProcessor = new ProductQueryProcessor(_unitOfWork.Object);
        }

        [Fact]
        public void GetByIdTest()
        {
            var product = new Product { ProductId = _product.ProductId };
            _productList.Add(product);

            var result = _queryProcessor.Get(product.ProductId);
            result.Should().Be(product);
        }

        [Fact]
        public async Task CreateProduct()
        {
            var entity = new ProductDtoCreate
            {
                ProductName = _product.ProductName,
                CategoryId = _product.CategoryId,
            };

            var result = await _queryProcessor.Create(entity, _token);

            result.ProductName.Should().Be(entity.ProductName);
            result.CategoryId.Should().Be(entity.CategoryId);
            _unitOfWork.Verify(x => x.Add(result, _token));
        }

        [Fact]
        public async Task UpdateProduct()
        {
            var product = new Product() { ProductId = _product.ProductId }; ;
            _productList.Add(product);

            var entity = new ProductDtoUpdate
            {
                ProductName = _product.ProductName,
            };

            var result = await _queryProcessor.Update(product.ProductId, entity, _token);

            result.Should().Be(product);
        }
    }
}