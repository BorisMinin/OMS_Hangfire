namespace OMS.API.Models.Dtos.CategoryDto
{
    /// <summary>
    /// Хранит в себе набор свойств, которые будут использоваться
    /// для создания новой сущности Category
    /// </summary>
    public class CategoryDtoCreate
    {
        // Наименование категории
        public string CategoryName { get; set; }
        // Описание категории
        public string Description { get; set; }
    }
}