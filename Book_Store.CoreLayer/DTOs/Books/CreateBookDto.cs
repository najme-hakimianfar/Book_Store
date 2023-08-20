using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.DTOs.Books
{
    public class CreateBookDto
    {
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }
        public string Publisher { get; set; }
        public string Writer { get; set; }
        public string Translator { get; set; }
        public int? PYear { get; set; }
        public int Price { get; set; }
        public int? NumberPage { get; set; }
        public string Size { get; set; }
        public string CoverType { get; set; }
        public int? Weight { get; set; }
        public IFormFile ImageFile { get; set; }
        public bool IsSpecial { get; set; }
        public int? Discount { get; set; }
    }
}
