using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.DataLayer.Entities
{
    public class Order:BaseEntity
    {
        public int UserId { get; set; }
        public int Sum { get; set; }
        public bool IsFinaly { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
