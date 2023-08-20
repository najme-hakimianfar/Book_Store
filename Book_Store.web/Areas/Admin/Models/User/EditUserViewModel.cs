using Book_Store.DataLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace Book_Store.web.Areas.Admin.Models.User
{
    public class EditUserViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FullName { get; set; }

        [MinLength(6, ErrorMessage = "{0}باید بیشتر از 5 کاراکتر باشد")]
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public UserRole Role { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Display(Name = "تلفن همراه")]
        public string Tel { get; set; }
    }
}
