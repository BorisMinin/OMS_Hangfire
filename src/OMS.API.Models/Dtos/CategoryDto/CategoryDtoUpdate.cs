namespace OMS.API.Models.Dtos.CategoryDto
{  
    /// <summary>
     /// Хранит в себе набор свойств, которые будут использоваться
     /// для обновления сущности Category
     /// </summary>
    public class CategoryDtoUpdate
    {
        // Идентификатор категории
        public int CategoryId { get; set; }
        // Наименование категории
        public string CategoryName { get; set; }
        // Описание категории
        public string Description { get; set; }
    }
}