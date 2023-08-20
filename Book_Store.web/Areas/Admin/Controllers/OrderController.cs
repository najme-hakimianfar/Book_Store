using Book_Store.CoreLayer.Services.Orders;
using Book_Store.DataLayer.Context;
using Book_Store.DataLayer.Entities;
using Book_Store.web.Areas.Admin.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Book_Store.CoreLayer.DTOs.Orders;

namespace Book_Store.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly DataBaseContext _context;

        public OrderController(IOrderService orderService, DataBaseContext context)
        {
            _orderService = orderService;
            _context = context;

        }

        public IActionResult AddToCart(int bookId,string location)
        {
            var CurrentUserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            _orderService.AddToCart(new CreateOrderDto()
            {
                UserId = CurrentUserId,
                BookId = bookId,
                IsFinaly = false
            });
            return Redirect(location);
        }

    
        public IActionResult Index()
        {
            var CurrentUserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var model=_orderService.GetOrderDetail(CurrentUserId);
            return View(model);
        }

        public IActionResult Delete(int orderDetailId)
        {
            _orderService.DeleteFromCart(orderDetailId);
            return RedirectToAction("Index");
        }

        public IActionResult Command(int orderDetailId,string command)
        {
            _orderService.Command(orderDetailId, command);
            return RedirectToAction("Index");
        }
    }
}


