using Book_Store.CoreLayer.DTOs.Categories;
using Book_Store.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.Mappers
{
    public class CategoryMapper
    {
        public static CategoryDto Map(Category category)
        {
            return new CategoryDto()
            {
                Slug = category.Slug,
                ParentId = category.ParentId,
                Title = category.Title,
                Id = category.Id
            };
        }
    }
}
