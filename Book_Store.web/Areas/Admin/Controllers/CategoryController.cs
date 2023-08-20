using Book_Store.CoreLayer.DTOs.Categories;
using Book_Store.CoreLayer.Services.Ctegories;
using Book_Store.CoreLayer.Utilities;
using Book_Store.web.Areas.Admin.Models.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View(_categoryService.GetAllCategory());
        }

        [Route("/admin/category/add/{parentId?}")]
        public IActionResult Add(int? parentId)
        {
            return View();
        }

        [HttpPost("/admin/category/add/{parentId?}")]
        public IActionResult Add(int? parentId, CreateCategoryViewModel createViewModel)
        {
            createViewModel.ParentId = parentId;
            var result = _categoryService.CreatCategory(createViewModel.MapToDto());
            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(createViewModel.Slug), result.Message);
                return View();
            }
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetCategoryBy(id);
            if (category == null)
                return RedirectToAction("Index");

            var model = new EditCategoryViewModel()
            {
                Slug = category.Slug,
                Title = category.Title
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditCategoryViewModel editmodel)
        {
            var result = _categoryService.EditCategory(new EditCategoryDto()
            {
                Slug = editmodel.Slug,
                Title = editmodel.Title,
                Id=id
                
            });

            if(result.Status !=OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(editmodel.Slug), result.Message);
                return View();
            }
            return RedirectToAction("Index");
            
        }

        public IActionResult GetChildCategories(int parentId)
        {
            var category = _categoryService.GetChildCategory(parentId);

            return new JsonResult(category);
        }
    }
}
