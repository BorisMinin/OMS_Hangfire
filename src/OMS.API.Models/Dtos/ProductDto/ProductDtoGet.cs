namespace OMS.API.Models.Dtos.ProductDto
{
    /// <summary>
    /// todo: написать комментарий
    /// </summary>
    public class ProductDtoGet
    {
        // todo: написать комментарий
        public int ProductID { get; set; }
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