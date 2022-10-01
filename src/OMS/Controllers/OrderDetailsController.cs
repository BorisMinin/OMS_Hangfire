using Microsoft.AspNetCore.Mvc;
using OMS.API.Models.Dtos.OrderDto;
using OMS.Maps;
using OMS.Queries.Interfaces;

namespace OMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderQueryProcessor _queryProcessor;
        private readonly IAutoMapper _autoMapper;

        public OrderController(IOrderQueryProcessor queryProcessor, IAutoMapper autoMapper)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
        }

        /// <summary>
        /// возвращает запись OrderDetails по идентификатору
        /// </summary>
        /// <param name="id">идентификатор детайлей заказа</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<OrderDtoGet> GetOrderDetail(int id, CancellationToken token)
        {
            var result = await this._queryProcessor.GetById(id, token);
            return this._autoMapper.Map<OrderDtoGet>(result);
        }

        /// <summary>
        /// запись в Order и в OrderDetails
        /// </summary>
        /// <param name="dtoOrder"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<OrderDtoCreate> CreateOrderDetail(OrderDtoCreate dtoOrder, CancellationToken token)
        {
            var result = await this._queryProcessor.Create(dtoOrder,token);
                
            return this._autoMapper.Map<OrderDtoCreate>(result);
        }

        /// <summary>
        /// Удаляет запись из таблицы OrderDetails, по id внешнего ключа каждой сущности
        /// </summary>
        /// <param name="orderId">first entity id</param>
        /// <param name="productId">second entity id</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("{orderId}/{productId}")]
        public async Task DeleteOrderDetail(int orderId, int productId, CancellationToken token)
        {
            await this._queryProcessor.Delete(orderId, productId, token);
        }
    }
}