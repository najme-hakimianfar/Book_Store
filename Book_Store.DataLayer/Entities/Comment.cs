using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.DataLayer.Entities
{
    public class Comment:BaseEntity
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        [Required]
        public string Text { get; set; }

        #region Relations
        public Book Book { get; set; }
        public User User { get; set; }
        #endregion
    }
}
