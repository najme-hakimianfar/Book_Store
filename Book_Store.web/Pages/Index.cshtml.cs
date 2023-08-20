using Book_Store.CoreLayer.DTOs;
using Book_Store.CoreLayer.DTOs.Books;
using Book_Store.CoreLayer.Services;
using Book_Store.CoreLayer.Services.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.web.Pages
{

    public class IndexModel : PageModel
    {
        private readonly IBooksService _bookService;
        private readonly IMainPageService _mainPageService;

        public IndexModel(IBooksService bookService, IMainPageService mainPageService)
        {
            _bookService = bookService;
            _mainPageService = mainPageService;
        }

        public MainPageDto MainPageData { set; get; }
        public void OnGet()
        {
            MainPageData = _mainPageService.GetData();
        }


    }
}
