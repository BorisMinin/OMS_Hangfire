using OMS.Data.Model.Entities;

namespace OMS.API.Models.Dtos.OrderDto
{
    /// <summary>
    /// Хранит в себе набор свойств, которые будут использоваться
    /// для получения записей сущности Order и/или OrderDetails
    /// </summary>
    public class OrderDtoGet
    {
        // todo: написать комментарий
        public int OrderId { get; set; }
        // todo: написать комментарий
        public DateTime? OrderDate { get; set; }
        // todo: написать комментарий
        public DateTime? RequiredDate { get; set; }
        // todo: написать комментарий
        public DateTime? ShippedDate { get; set; }
        // todo: написать комментарий
        public int? ShipVia { get; set; }
        // todo: написать комментарий
        public decimal? Freight { get; set; }
        // todo: написать комментарий
        public string? ShipName { get; set; }
        // todo: написать комментарий
        public string? ShipAddress { get; set; }
        // todo: написать комментарий
        public string? ShipCity { get; set; }
        // todo: написать комментарий
        public string? ShipRegion { get; set; }
        // todo: написать комментарий
        public string? ShipPostalCode { get; set; }
        // todo: написать комментарий
        public string? ShipCountry { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}