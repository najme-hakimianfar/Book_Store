using Book_Store.CoreLayer.DTOs.Categories;
using Book_Store.CoreLayer.Mappers;
using Book_Store.CoreLayer.Utilities;
using Book_Store.DataLayer.Context;
using Book_Store.DataLayer.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Book_Store.CoreLayer.Services.Ctegories
{
    public class CategoryService : ICategoryService
    {
        private readonly DataBaseContext _context;

        public CategoryService(DataBaseContext context)
        {
            _context = context;
        }
        public OperationResult CreatCategory(CreatCategoryDto command)
        {
            var isSlugExist= _context.Categories.Any(c => c.Slug ==command.Slug.ToSlug());
            if (isSlugExist)
                return OperationResult.Error("این slug وجود دارد");

            var category = new Category()
            {
                Title = command.Title,
                IsDelete = false,
                ParentId = command.ParentId,
                Slug = command.Slug.ToSlug(),                
                
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult EditCategory(EditCategoryDto command)
        {
            var isSlugExist = _context.Categories.Any(c => c.Slug == command.Slug.ToSlug());
            var category = _context.Categories.FirstOrDefault(c => c.Id == command.Id);

            if (category == null)
                return OperationResult.NotFound();

            if(command.Slug.ToSlug() !=category.Slug)
                if (isSlugExist)
                    return OperationResult.Error("Slug is Exist");

            category.Slug = command.Slug.ToSlug();
            category.Title = command.Title;

        
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public List<CategoryDto> GetAllCategory()
        {
            return _context.Categories.Select(category => CategoryMapper.Map(category)).ToList();
        }

        public CategoryDto GetCategoryBy(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return null;
            return CategoryMapper.Map(category);
        }

        public CategoryDto GetCategoryBy(string slug)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Slug==slug);
            if (category == null)
                return null;
            return CategoryMapper.Map(category);
        }

        public List<CategoryDto> GetChildCategory(int parentId)
        {
            return _context.Categories.Where(r=> r.ParentId==parentId)
                .Select(category => CategoryMapper.Map(category)).ToList();
        }
    }
}
