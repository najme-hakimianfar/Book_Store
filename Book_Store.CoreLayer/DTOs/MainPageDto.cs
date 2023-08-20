using Book_Store.CoreLayer.DTOs.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.DTOs
{
    public class MainPageDto
    {
        public List<BookDto> LatestBook { get; set; }
        public List<BookDto> SpecialBook { get; set; }
        public List<BookDto> DiscountBook { get; set; }
    }
}
