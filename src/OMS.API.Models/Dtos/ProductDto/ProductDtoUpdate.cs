namespace OMS.API.Models.Dtos.ProductDto
{
    /// <summary>
    /// Хранит в себе набор свойств, которые будут использоваться
    /// для создания новой сущности Product
    /// </summary>
    public class ProductDtoUpdate
    {
        // todo: написать комментарий
        public string ProductName { get; set; }
        // todo: написать комментарий
        public int? CategoryID { get; set; }
        // todo: написать комментарий
        public string QuantityPerUnit { get; set; }
        // todo: написать комментарий
        public decimal? UnitPrice { get; set; }
        // todo: написать комментарий
        public short? UnitsInStock { get; set; }
        // todo: написать комментарий
        public short? UnitsOnOrder { get; set; }
        // todo: написать комментарий
        public short? ReorderLevel { get; set; }
        // todo: написать комментарий
        public bool Discontinued { get; set; }
    }
}