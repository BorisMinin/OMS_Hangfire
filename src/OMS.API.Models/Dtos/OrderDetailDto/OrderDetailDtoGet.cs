using OMS.Data.Model.Entities;

namespace OMS.API.Models.OrderDetailDto
{
    public class OrderDetailDtoGet
    {
        // todo: написать комментарий
        public int OrderId { get; set; }
        // todo: написать комментарий
        public int ProductId { get; set; }
        // todo: написать комментарий
        public decimal UnitPrice { get; set; }
        // todo: написать комментарий
        public short Quantity { get; set; }
        // todo: написать комментарий
        public float Discount { get; set; }
        // todo: написать комментарий
        public Order Order { get; set; }
        // todo: написать комментарий
        public Product Product { get; set; }
    }
}