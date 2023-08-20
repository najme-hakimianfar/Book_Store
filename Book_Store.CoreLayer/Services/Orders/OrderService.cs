using Book_Store.CoreLayer.DTOs.Orders;
using Book_Store.CoreLayer.Utilities;
using Book_Store.DataLayer.Context;
using Book_Store.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Book_Store.CoreLayer.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly DataBaseContext _context;

        public OrderService(DataBaseContext context)
        {
            _context = context;
        }
        public OperationResult AddToCart(CreateOrderDto command)
        {
            var order = _context.Orders.SingleOrDefault(o => o.UserId == command.UserId);
            if (order == null)
            {
                order = new Order()
                {
                    UserId = command.UserId,
                    IsFinaly = false,
                    Sum = 0,
                };
                _context.Orders.Add(order);
                _context.SaveChanges();

                var orderDetailes = new OrderDetail()
                {
                    OrderId = order.Id,
                    Count = 1,
                    Price = _context.Books.Find(command.BookId).Price,
                    BookId = command.BookId
                };
                _context.OrderDetails.Add(orderDetailes);
                _context.SaveChanges();
            }
            else
            {
                var orderDetailes = _context.OrderDetails.SingleOrDefault(d => d.OrderId == order.Id && d.BookId == command.BookId);
                if (orderDetailes == null)
                {
                    orderDetailes = new OrderDetail()
                    {
                        OrderId = order.Id,
                        Count = 1,
                        Price = _context.Books.Find(command.BookId).Price,
                        BookId = command.BookId
                    };

                    _context.OrderDetails.Add(orderDetailes);
                }
                else
                {
                    orderDetailes.Count += 1;
                }
                order = _context.Orders.Find(order.Id);
                order.Sum = _context.OrderDetails.Where(o => o.OrderId == order.Id).Select(d => d.Count * d.Price).Sum();               
                _context.SaveChanges();
            }
            return OperationResult.Success();
        }

        public void Command(int orderDetailId, string command)
        {
            var orderDetail = _context.OrderDetails.Find(orderDetailId);

            switch (command)
            {
                case "up":
                    {
                        orderDetail.Count += 1;
                        _context.SaveChanges();
                        break;
                    }
                case "down":
                    {
                        orderDetail.Count -= 1;
                        if (orderDetail.Count == 0)
                            orderDetail.IsDelete = true;
                        else
                            _context.SaveChanges();
                    }
                    break;
            }
            _context.SaveChanges();
        }

        public OperationResult DeleteFromCart(int orderDetailId)
        {
            var orderDetail = _context.OrderDetails.Find(orderDetailId);

            if (orderDetail == null)
                return OperationResult.NotFound();

            orderDetail.IsDelete = true;
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public List<OrderDto> GetOrderDetail(int userId)
        {
            var order = _context.Orders.SingleOrDefault(o => o.UserId ==userId  && !o.IsFinaly);

            List<OrderDto> _list = new List<OrderDto>();

            if (order != null)
            {
                var details = _context.OrderDetails.Where(d => d.OrderId == order.Id).ToList();
                foreach (var item in details)
                {
                    var book = _context.Books.Find(item.BookId);
                    _list.Add(new OrderDto()
                    {
                        Count = item.Count,
                        ImageName = book.ImageName,
                        OrderDetailId = item.Id,
                        Price = item.Price,
                        Sum = item.Count * item.Price,
                        Name = book.Name
                    });
                }
            }
            return _list;
        }
    }
}
