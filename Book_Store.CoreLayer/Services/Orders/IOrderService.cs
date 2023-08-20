using Book_Store.CoreLayer.DTOs.Orders;
using Book_Store.CoreLayer.Utilities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.Services.Orders
{
    public interface IOrderService
    {
        OperationResult AddToCart(CreateOrderDto command);
        OperationResult DeleteFromCart(int orderDetailId);
        List<OrderDto> GetOrderDetail(int userId);
        void Command(int orderDetailId,string command);
    }
}
