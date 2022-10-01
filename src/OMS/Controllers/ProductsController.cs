using Microsoft.AspNetCore.Mvc;
using OMS.API.Models.Dtos.ProductDto;
using OMS.Maps;
using OMS.Queries.Interfaces;

namespace OMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductQueryProcessor _queryProcessor;
        private readonly IAutoMapper _autoMapper;

        public ProductsController(IProductQueryProcessor queryProcessor, IAutoMapper autoMapper)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
        }

        /// <summary>
        /// возвращает запись Product по идентификатору
        /// </summary>
        /// <param name="id">идентификатор Product</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ProductDtoGet GetProduct(int id)
        {
            var result = this._queryProcessor.Get(id);
            return this._autoMapper.Map<ProductDtoGet>(result);
        }

        /// <summary>
        /// todo: написать комментарий
        /// </summary>
        /// <param name="dtoProduct"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ProductDtoGet> AddProduct([FromBody] ProductDtoCreate dtoProduct, CancellationToken token)
        {
            var result = await this._queryProcessor.Create(dtoProduct, token);
            return this._autoMapper.Map<ProductDtoGet>(result);
        }

        /// <summary>
        /// todo: написать комментарий
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dtoProduct"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ProductDtoGet> UpdateProduct(int id, [FromBody] ProductDtoUpdate dtoProduct, CancellationToken token)
        {
            var result = await this._queryProcessor.Update(id, dtoProduct, token);
            return this._autoMapper.Map<ProductDtoGet>(result);
        }

        /// <summary>
        /// удаляет запись Product по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task DeleteProduct(int id, CancellationToken token)
        {
            await this._queryProcessor.Delete(id, token);
        }
    }
}