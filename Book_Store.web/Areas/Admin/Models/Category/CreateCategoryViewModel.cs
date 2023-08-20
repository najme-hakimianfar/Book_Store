using Book_Store.CoreLayer.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.web.Areas.Admin.Models.Category
{
    public class CreateCategoryViewModel
    {

        [Display(Name ="عنوان")]
        [Required(ErrorMessage ="وارد کردن {0} اجباری است")]

        public string Title { get; set; }

        [Display(Name ="Slug")]
        [Required(ErrorMessage ="وارد کردن {0} اجباری است")]
        public string Slug { get; set; }

        //[Display(Name = "MetaTag(با - ازهم جدا کنیذ)")]
        //public string MetaTag { get; set; }

        //[DataType(DataType.MultilineText)]
        //public string MetaDescription { get; set; }
        public int? ParentId { get; set; }

        public CreatCategoryDto MapToDto()
        {
            return new CreatCategoryDto()
            {
                Title=Title,
                //MetaDescription=MetaDescription,
                //MetaTag=MetaTag,
                Slug=Slug,
                ParentId=ParentId
            };
        }
    }
}
