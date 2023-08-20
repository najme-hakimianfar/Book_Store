using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.DTOs.Categories
{
    public class CreatCategoryDto
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public int? ParentId { get; set; }
    }
}
