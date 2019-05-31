using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.DTO;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Servcies;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _CartService;
        private readonly IOrderService _OrderService;
        private readonly ILogger<CartController> _Logger;

        public CartController(ICartService CartService, IOrderService OrderService, ILogger<CartController> Logger)
        {
            _CartService = CartService;
            _OrderService = OrderService;
            _Logger = Logger;
        }

        public IActionResult Details()
        {
            var model = new DetailsViewModel
            {
                CartViewModel = _CartService.TransformCart(),
                OrderViewModel = new OrderViewModel()
            };
            return View(model);
        }

        public IActionResult GetCartView() => ViewComponent("Cart");

        public IActionResult AddToCart(int id)
        {
            _CartService.AddToCart(id);
            return RedirectToAction("Details");
        }

        public IActionResult AddToCartAJAX(int id)
        {
            _CartService.AddToCart(id);
            return Json(new { id, message = $"Товар {id} добавлен к корзине" });
        }

        public IActionResult DecrementFromCart(int id)
        {
            _CartService.DecrementFromCart(id);
            return RedirectToAction("Details");
        }

        public IActionResult DecrementFromCartAJAX(int id)
        {
            _CartService.DecrementFromCart(id);
            return Json(new { id, message = $"Количество товара {id} в корзине уменьшено на 1" });
        }

        public IActionResult RemoveFromCart(int id)
        {
            _CartService.RemoveFromCart(id);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveFromCartAJAX(int id)
        {
            _CartService.RemoveFromCart(id);
            return Json(new { id, message = $"Товар {id} удалён из корзины" });
        }

        public IActionResult RemoveAll()
        {
            _CartService.RemoveAll();
            return RedirectToAction("Details");
        }

        public IActionResult RemoveAllAJAX()
        {
            _CartService.RemoveAll();
            return Json(new { message = "Корзина очищена" });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CheckOut(OrderViewModel model)
        {
            if (!ModelState.IsValid)
                return View(nameof(Details), new DetailsViewModel
                {
                    CartViewModel = _CartService.TransformCart(),
                    OrderViewModel = model
                });

            using (_Logger.BeginScope("Оформление заказа для пользователя {0}", User.Identity.Name))
            {
                var create_order_model = new CreateOrderModel
                {
                    OrderViewModel = model,
                    OrderItems = _CartService.TransformCart().Items.Select(item => new OrderItemDTO
                    {
                        Id = item.Key.Id,
                        Price = item.Key.Price,
                        Quantity = item.Value
                    }).ToList()
                };

                var order = _OrderService.CreateOrder(create_order_model, User.Identity.Name);
                _Logger.LogInformation("Сформирован заказ id:{0}", order.Id);

                _CartService.RemoveAll();
                _Logger.LogInformation("Корзина пользователя очищена", order.Id);

                return RedirectToAction("OrderConfirmed", new { id = order.Id });
            }                                
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}