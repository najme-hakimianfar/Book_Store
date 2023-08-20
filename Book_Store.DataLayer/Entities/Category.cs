using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.DataLayer.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Slug { get; set; }
        public int? ParentId { get; set; }

        #region Relation
        [InverseProperty("Category")]
        public virtual ICollection<Book> Books { get; set; }

        [InverseProperty("SubCategory")]
        public virtual ICollection<Book> SubBooks { get; set; }
        #endregion
    }
}
