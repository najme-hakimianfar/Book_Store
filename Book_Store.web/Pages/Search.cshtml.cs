using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.CoreLayer.DTOs.Books;
using Book_Store.CoreLayer.Services.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Book_Store.web.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IBooksService _bookService;

        public SearchModel(IBooksService bookService)
        {
            _bookService = bookService;
        }
        public BookFilterDto Filter { get; set; }
        public void OnGet(int pageId=1,string categorySlug=null,string q=null)
        {
            Filter = _bookService.GetBookByFilter(pageId, 4, q, categorySlug);
            
        }
    }
}
