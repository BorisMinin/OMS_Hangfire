namespace OMS.Data.Model.Entities
{
    //todo: добавить комментарий к сущности, описав её семантику
    public class Category
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