using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.DTOs.Orders
{
    public class CreateOrderDto
    {

        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Sum { get; set; }
        public bool IsFinaly { get; set; }
    }
}
