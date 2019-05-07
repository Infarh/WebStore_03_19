using System.Collections.Generic;
using WebStore.Domain.DTO;

namespace WebStore.Domain.ViewModels
{
    public class CreateOrderModel
    {
        public OrderViewModel OrderViewModel { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; }
    }
}