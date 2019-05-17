using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.DTO;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Servcies;

namespace WebStore.ServiceHosting.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/orders")]
    [ApiController]
    [Produces("application/json")]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _OrderService;
        private readonly ILogger<OrdersApiController> _Logger;

        public OrdersApiController(IOrderService OrderService, ILogger<OrdersApiController> Logger)
        {
            _OrderService = OrderService;
            _Logger = Logger;
        }

        [HttpGet("user/{UserName}")]
        public IEnumerable<OrderDTO> GetUserOrders(string UserName) => _OrderService.GetUserOrders(UserName);

        [HttpGet("{id}"), ActionName("Get")]
        public OrderDTO GetOrderById(int id) => _OrderService.GetOrderById(id);

        [HttpPost("{UserName?}")]
        public OrderDTO CreateOrder([FromBody] CreateOrderModel OrderModel, string UserName)
        {
            using (_Logger.BeginScope("Создание заказа для {0}", UserName))
            {
                var order = _OrderService.CreateOrder(OrderModel, UserName);
                _Logger.LogInformation("Заказ {0} для пользователя {1} успешно сформирован", order.Id, UserName);
                return order;
            }
        }
    }
}