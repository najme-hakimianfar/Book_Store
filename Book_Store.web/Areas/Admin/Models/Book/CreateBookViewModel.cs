using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Book_Store.web.Areas.Admin.Models.Book
{
    public class CreateBookViewModel
    {
        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryId { get; set; }

        [Display(Name = "زیر دسته بندی")]
        public int? SubCategoryId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "slug")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Slug { get; set; }

        [Display(Name = "ناشر")]
        public string Publisher { get; set; }

        [Display(Name = "نویسنده")]
        public string Writer { get; set; }

        [Display(Name = "مترجم")]
        public string Translator { get; set; }

        [Display(Name = "سال انتشار")]
        public int? PYear { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }

        [Display(Name = "تعداد صفحه")]
        public int? NumberPage { get; set; }

        [Display(Name = "قطع")]
        public string Size { get; set; }

        [Display(Name = "نوع جلد")]
        public string CoverType { get; set; }

        [Display(Name = "وزن")]
        public int? Weight { get; set; }

        [Display(Name = "عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "تخفیف")]
        public int? Discount { get; set; }

        [Display(Name = "کتاب ویژه")]
        public bool IsSpecial { get; set; }


    }

}
