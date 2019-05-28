using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebStore.Clients.Base;
using WebStore.Domain.DTO;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Servcies;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        private readonly ILogger<OrdersClient> _Logger;
        public OrdersClient(IConfiguration Configuration, ILogger<OrdersClient> Logger) : base(Configuration, "api/orders") => _Logger = Logger;

        public IEnumerable<OrderDTO> GetUserOrders(string UserName) => Get<List<OrderDTO>>($"{ServiceAddress}/user/{UserName}");

        public IEnumerable<OrderDTO> GetAll() => Get<List<OrderDTO>>($"{ServiceAddress}");

        public OrderDTO GetOrderById(int id) => Get<OrderDTO>($"{ServiceAddress}/{id}");

        public OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName)
        {
            _Logger.LogInformation("Запрос к WebAPI для формирования заказа {0} пользователя {1}", OrderModel.OrderViewModel.Name, UserName);
            try
            {
                var response = Post($"{ServiceAddress}/{UserName}", OrderModel);
                var order = response.Content.ReadAsAsync<OrderDTO>().Result;
                _Logger.LogInformation("Заказ {0} сформирован", order.Id);
                return order;
            }   
            catch (Exception error) //todo: Кконкретизировать тип исключения
            {
                _Logger.LogError("Ошибка формирования заказа {0} для пользователя {1}: {2}", OrderModel.OrderViewModel.Name, UserName, error.ToString());
                throw;
            }
        }
    }
}
