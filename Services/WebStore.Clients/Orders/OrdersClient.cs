using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.DTO;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Servcies;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration Configuration) : base(Configuration, "api/orders") { }

        public IEnumerable<OrderDTO> GetUserOrders(string UserName) => Get<List<OrderDTO>>($"{ServiceAddress}/user/{UserName}");

        public OrderDTO GetOrderById(int id) => Get<OrderDTO>($"{ServiceAddress}/{id}");

        public OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName)
        {
            var response = Post($"{ServiceAddress}/{UserName}", OrderModel);
            return response.Content.ReadAsAsync<OrderDTO>().Result;
        }
    }
}
