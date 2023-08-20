using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.DataLayer.Entities
{
    public class Book : BaseEntity
    {
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
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
        public string ImageName { get; set; }
        public bool IsSpecial { get; set; }
        public int? Discount { get; set; }

        #region Relations
        [ForeignKey("CategoryId")]
        public  Category Category { get; set; }

        [ForeignKey("SubCategoryId")]
        public Category SubCategory { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
        public ICollection<Comment> Comments { get; set; }
        #endregion
    }
}
