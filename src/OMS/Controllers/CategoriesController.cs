using Microsoft.AspNetCore.Mvc;
using OMS.API.Models.Dtos.CategoryDto;
using OMS.Maps;
using OMS.Queries.Interfaces;

namespace OMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryQueryProcessor _queryProcessor;
        private readonly IAutoMapper _autoMapper;

        public CategoriesController(ICategoryQueryProcessor queryProcessor, IAutoMapper autoMapper)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
        }

        /// <summary>
        /// возвращает запись Category по идентификатору
        /// </summary>
        /// <param name="id">идентификатор категории</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public CategoryDtoGet GetCategory(int id)
        {
            var result = this._queryProcessor.Get(id);
            return this._autoMapper.Map<CategoryDtoGet>(result);
        }

        /// <summary>
        /// todo: написать комментарий
        /// </summary>
        /// <param name="dtoCategory"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<CategoryDtoGet> AddCategory([FromBody] CategoryDtoCreate dtoCategory, CancellationToken token)
        {
            var result = await this._queryProcessor.Create(dtoCategory, token);
            return this._autoMapper.Map<CategoryDtoGet>(result);
        }

        /// <summary>
        /// todo: написать комментарий
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dtoCategory"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<CategoryDtoGet> UpdateCategory(int id, [FromBody] CategoryDtoUpdate dtoCategory, CancellationToken token)
        {
            var result = await this._queryProcessor.Update(id, dtoCategory, token);
            return this._autoMapper.Map<CategoryDtoGet>(result);
        }

        /// <summary>
        /// удаляет запись Category по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task DeleteCategory(int id, CancellationToken token)
        {
            await this._queryProcessor.Delete(id, token);
        }
    }
}