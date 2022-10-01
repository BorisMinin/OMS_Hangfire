using OMS.API.Models.Dtos.ProductDto;
using OMS.Data.Model.Entities;

namespace OMS.Queries.Interfaces
{
    public interface IProductQueryProcessor
    {
        IQueryable<Product> Get();
        Product Get(int id);
        Task<Product> Create(ProductDtoCreate dto, CancellationToken token);
        Task<Product> Update(int id, ProductDtoUpdate dto, CancellationToken token);
        Task Delete(int id, CancellationToken token);
    }
}