﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.DAL.Context;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Servcies;

namespace WebStore.Services.Sql
{
    public class SqlOrdersService : IOrderService
    {
        private readonly WebStoreContext _db;
        private readonly UserManager<User> _UserManager;
        private readonly ILogger<SqlOrdersService> _Logger;

        public SqlOrdersService(WebStoreContext db, UserManager<User> UserManager, ILogger<SqlOrdersService> Logger)
        {
            _db = db;
            _UserManager = UserManager;
            _Logger = Logger;
        }

        public IEnumerable<OrderDTO> GetAll() =>
            _db.Orders
               .Select(order => new OrderDTO
                {
                    Id = order.Id,
                    Name = order.Name,
                    Phone = order.Phone,
                    Address = order.Address,
                    Date = order.Date,
                    OrderItems = order.OrderItems.Select(item => new OrderItemDTO
                        {
                            Id = item.Id,
                            Price = item.Price,
                            Quantity = item.Quantity
                        })
                       .ToArray()
                })
               .ToArray();

        public IEnumerable<OrderDTO> GetUserOrders(string UserName) =>
            _db.Orders
                .Include(order => order.User)
                .Include(order => order.OrderItems)
                .Where(order => order.User.UserName == UserName)
                .Select(order => new OrderDTO
                {
                    Id = order.Id,
                    Name = order.Name,
                    Address = order.Address,
                    Phone = order.Phone,
                    Date = order.Date,
                    OrderItems = order.OrderItems.Select(item => new OrderItemDTO
                    {
                        Id = item.Id,
                        Price = item.Price,
                        Quantity = item.Quantity
                    }).ToArray()
                })
                .ToArray();

        public OrderDTO GetOrderById(int id)
        {
            var order = _db.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == id);
            if (order is null) return null;
            return new OrderDTO
            {
                Id = order.Id,
                Name = order.Name,
                Address = order.Address,
                Phone = order.Phone,
                Date = order.Date,
                OrderItems = order.OrderItems.Select(item => new OrderItemDTO
                {
                    Id = item.Id,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToArray()
            };
        }

        public OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName)
        {
            var user = _UserManager.FindByNameAsync(UserName).Result;

            //_Logger.LogInformation("Подготовка данынх заказа");

            using (var transaction = _db.Database.BeginTransaction())
            {
                var order = new Order
                {
                    Name = OrderModel.OrderViewModel.Name,
                    Address = OrderModel.OrderViewModel.Address,
                    Phone = OrderModel.OrderViewModel.Phone,
                    User = user,
                    Date = DateTime.Now
                };

                _db.Orders.Add(order);

                foreach (var item in OrderModel.OrderItems)
                {

                    var product = _db.Products.FirstOrDefault(p => p.Id == item.Id);
                    if(product is null)
                        throw new InvalidOperationException($"Товар с идентификатором {item.Id} в базе данных не найден");

                    var order_item = new OrderItem
                    {
                        Order = order,
                        Price = product.Price,
                        Quantity = item.Quantity,
                        Product = product
                    };

                    _db.OrderItems.Add(order_item);
                }

                _db.SaveChanges();
                transaction.Commit();

                _Logger.LogInformation("Информация о заказе {0} внесена в БД", order.Id);

                return new OrderDTO
                {
                    Id = order.Id,
                    Name = order.Name,
                    Address = order.Address,
                    Phone = order.Phone,
                    Date = order.Date,
                    OrderItems = order.OrderItems.Select(item => new OrderItemDTO
                    {
                        Id = item.Id,
                        Price = item.Price,
                        Quantity = item.Quantity
                    }).ToArray()
                };
            }
        }
    }
}
