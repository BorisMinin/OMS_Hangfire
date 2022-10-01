using OMS.API.Models.Dtos.OrderDto;
using OMS.Data.Model.Entities;

namespace OMS.Queries.Interfaces
{
    public interface IOrderQueryProcessor
    {
        IQueryable<Order> Get(CancellationToken token);

        Task<Order> GetById(int id, CancellationToken token);

        Task<Order> Create(OrderDtoCreate orderDto, CancellationToken token);

        Task<Order> Update(int id, OrderDtoUpdate dto, CancellationToken token);

        Task Delete(int orderId, int productId, CancellationToken token);
    }
}