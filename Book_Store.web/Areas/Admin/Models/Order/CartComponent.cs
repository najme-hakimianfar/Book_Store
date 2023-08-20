using Book_Store.CoreLayer.DTOs.Orders;
using Book_Store.DataLayer.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Book_Store.web.Areas.Admin.Models.Order
{
    public class CartComponent : ViewComponent
    {
        private readonly DataBaseContext _context;

        public CartComponent(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<OrderDto> _list = new List<OrderDto>();

            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                var order = _context.Orders.SingleOrDefault(o => o.UserId == currentUserId && !o.IsFinaly);
                if (order != null)
                {
                    var details = _context.OrderDetails.Where(d => d.OrderId == order.Id).ToList();
                    foreach (var item in details)
                    {
                        var book = _context.Books.Find(item.BookId);
                        var user = _context.Users.Find(currentUserId);
                        _list.Add(new OrderDto()
                        {
                            Count = item.Count,
                            Name = book.Name,
                            ImageName = book.ImageName
                        });
                    }
                }
            }
            return View(viewName:"/Pages/Shared/_ShowCart.cshtml",model:_list);
        }
    }
}
