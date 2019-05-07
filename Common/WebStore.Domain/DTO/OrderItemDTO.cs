using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.DTO
{
    public class OrderItemDTO : BaseEntity 
    {
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }

    public class OrderDTO : NamedEntity
    {
        public string Phone { get; set; }  
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<OrderItemDTO> OrderItems { get; set; }
    }
}
