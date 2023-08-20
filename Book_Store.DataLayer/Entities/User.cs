using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.DataLayer.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
    public enum UserRole
    {
        Admin,
        Customer
    }
}
