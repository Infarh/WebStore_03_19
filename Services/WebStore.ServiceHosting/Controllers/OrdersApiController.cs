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
    /// <summary>Контроллер сервиса управления заказами</summary>
    //[Route("api/[controller]")]
    [Route("api/orders")]
    [ApiController]
    [Produces("application/json")]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _OrderService;
        private readonly ILogger<OrdersApiController> _Logger;

        /// <summary>Инициализация контроллера управления сервисами заказов</summary>
        /// <param name="OrderService">Сервис заказов</param>
        /// <param name="Logger">Логгер</param>
        public OrdersApiController(IOrderService OrderService, ILogger<OrdersApiController> Logger)
        {
            _OrderService = OrderService;
            _Logger = Logger;
        }

        /// <summary>Получить все заказы</summary>
        /// <remarks>Выдаёт последовательность всех заказов, хранимых в источнике данных сервиса</remarks>
        /// <code>GetAll();</code>
        /// <returns>Перечисление заказов</returns>
        [HttpGet("Get")]
        public IEnumerable<OrderDTO> GetAll() => _OrderService.GetAll();

        /// <summary>Получить все заказы указанного пользователя</summary>
        /// <param name="UserName">Имя пользователя, заказы которого требуется извлечь</param>
        /// <returns>Перечисление заказов указанного пользователя</returns>
        [HttpGet("user/{UserName}")]
        public IEnumerable<OrderDTO> GetUserOrders(string UserName) => _OrderService.GetUserOrders(UserName);

        /// <summary>Получить заказ с указанным идентификатором</summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Заказ с указанным идентификатором, либо пустая ссылка</returns>
        [HttpGet("{id}"), ActionName("Get")]
        public OrderDTO GetOrderById(int id) => _OrderService.GetOrderById(id);

        /// <summary>Создать заказ для указанного пользователя</summary>
        /// <param name="OrderModel">Информация о заказе</param>
        /// <param name="UserName">Имя пользователя, для которого требуется сформировать заказ</param>
        /// <returns>Сформированный заказ</returns>
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