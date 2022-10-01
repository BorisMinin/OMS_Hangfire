using OMS.Data.Model.Entities;

namespace OMS.API.Models.Dtos.CategoryDto
{
    /// <summary>
    /// Хранит в себе набор свойств, которые будут использоваться
    /// для получения записи(записей) сущности Category
    /// </summary>
    public class CategoryDtoGet
    {
        // Идентификатор категории
        public int CategoryId { get; set; }
        // Наименование категории
        public string CategoryName { get; set; }
        // Описание категории
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
