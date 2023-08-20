using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.DataLayer.Entities
{
    public class OrderDetail:BaseEntity
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }

        public Order Order { get; set; }
        public Book Book { get; set; }
    }
}
