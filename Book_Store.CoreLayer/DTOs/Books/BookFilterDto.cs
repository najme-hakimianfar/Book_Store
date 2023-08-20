using Book_Store.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.DTOs.Books
{
    public class BookFilterDto:BasePagination
    {
        public List<BookDto> Books { get; set; }
        public string Name { get; set; }
        public string CategorySlug { get; set; }
        public int PageId { get; set; }
        public int Take { get; set; }

    }




}
