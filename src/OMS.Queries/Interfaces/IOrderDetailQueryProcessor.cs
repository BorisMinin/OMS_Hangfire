using OMS.API.Models.OrderDetailDto;
using OMS.Data.Model.Entities;

namespace OMS.Queries.Interfaces
{
    public interface IOrderDetailQueryProcessor
    {
        IQueryable<OrderDetail> Get(CancellationToken token);

        Task<OrderDetail> GetById(int id, CancellationToken token);

        Task<OrderDetail> Create(OrderDetailDtoCreate orderDto, CancellationToken token);

        Task<OrderDetail> Update(int id, OrderDetailDtoUpdate dto, CancellationToken token);

        Task Delete(int id, CancellationToken token);
    }
}