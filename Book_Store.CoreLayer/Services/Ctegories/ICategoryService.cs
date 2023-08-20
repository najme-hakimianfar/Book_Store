using Book_Store.CoreLayer.DTOs.Categories;
using Book_Store.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.Services.Ctegories
{
    public interface ICategoryService
    {
        OperationResult CreatCategory(CreatCategoryDto command);
        OperationResult EditCategory(EditCategoryDto command);
        List<CategoryDto> GetAllCategory();
        List<CategoryDto> GetChildCategory(int parentId);
        CategoryDto GetCategoryBy(int id);
        CategoryDto GetCategoryBy(string slug);
    }
}
