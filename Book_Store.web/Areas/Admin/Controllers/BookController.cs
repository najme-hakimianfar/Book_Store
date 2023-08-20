using Book_Store.CoreLayer.DTOs.Books;
using Book_Store.CoreLayer.Services.Books;
using Book_Store.CoreLayer.Utilities;
using Book_Store.web.Areas.Admin.Models.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.web.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly IBooksService _booksService;
        public BookController(IBooksService booksService)
        {
            _booksService = booksService;
        }
        public IActionResult Index(int pageId=1,string name="",string categorySlug="")
        {
            var model = _booksService.GetBookByFilter(pageId,5,name,categorySlug);
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreateBookViewModel createViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(createViewModel);
            }
            var result = _booksService.CreateBook(new CreateBookDto()
            {
                CategoryId=createViewModel.CategoryId,
                SubCategoryId=createViewModel.SubCategoryId==0?null: createViewModel.SubCategoryId,
                Name=createViewModel.Name,
                Slug=createViewModel.Slug,
                Publisher=createViewModel.Publisher,
                Writer=createViewModel.Writer,
                Translator=createViewModel.Translator,
                PYear=createViewModel.PYear,
                Price=createViewModel.Price,
                NumberPage=createViewModel.NumberPage,
                CoverType=createViewModel.CoverType,
                Size=createViewModel.Size,
                Weight=createViewModel.Weight,
                ImageFile=createViewModel.ImageFile,
                IsSpecial=createViewModel.IsSpecial,
                Discount=createViewModel.Discount
               
            });
            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(createViewModel.Slug), result.Message);
                return View(createViewModel);
            }
          
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var book = _booksService.GetBookById(id);
            if (book == null)
                return RedirectToAction("Index");
            var model = new EditBookViewModel()
            {
                CategoryId = book.CategoryId,
                SubCategoryId = book.SubCategoryId,
                Name = book.Name,
                Slug = book.Slug,
                Publisher = book.Publisher,
                Writer = book.Writer,
                Translator = book.Translator,
                PYear = book.PYear,
                Price = book.Price,
                NumberPage = book.NumberPage,
                Size = book.Size,
                CoverType = book.CoverType,
                Weight = book.Weight,
                IsSpecial=book.IsSpecial,
                Discount=book.Discount
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,EditBookViewModel editViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editViewModel);
            }
            var result = _booksService.EditBook(new EditBookDto()
            {
                CategoryId = editViewModel.CategoryId,
                SubCategoryId = editViewModel.SubCategoryId == 0 ? null : editViewModel.SubCategoryId,
                Name = editViewModel.Name,
                Slug = editViewModel.Slug,
                Publisher = editViewModel.Publisher,
                Writer = editViewModel.Writer,
                Translator = editViewModel.Translator,
                PYear = editViewModel.PYear,
                Price = editViewModel.Price,
                NumberPage = editViewModel.NumberPage,
                CoverType = editViewModel.CoverType,
                Size = editViewModel.Size,
                Weight = editViewModel.Weight,
                ImageFile = editViewModel.ImageFile,
                BookId=id,
                IsSpecial=editViewModel.IsSpecial,
                Discount=editViewModel.Discount
            });
            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(EditBookViewModel.Slug), result.Message);
                return View(editViewModel);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int bookId)
        {
            _booksService.DeleteBook(bookId);
            return RedirectToAction("index");
        }
    }
}
