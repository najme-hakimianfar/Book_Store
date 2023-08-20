using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Book_Store.CoreLayer.DTOs.Books;
using Book_Store.CoreLayer.DTOs.Comments;
using Book_Store.CoreLayer.Services.Books;
using Book_Store.CoreLayer.Services.Comments;
using Book_Store.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Book_Store.web.Pages
{
    [BindProperties]
    [ValidateAntiForgeryToken]
    public class BookModel : PageModel
    {
        private readonly IBooksService _booksService;
        private readonly ICommentService _commentService;
        public BookModel(IBooksService booksService,ICommentService commentService)
        {
            _booksService = booksService;
            _commentService = commentService;
        }

        public BookDto Book { set;get; }

        [Display(Name ="متن دیدگاه")]
        [Required(ErrorMessage ="وارد کردن {0} اجباری است")]
        public string Text { get; set; }
        public int BookId { get; set; }
        public  List<CommentDto> Comments { set; get; }

        public IActionResult OnGet(string slug)
        {
            Book = _booksService.GetBookBySlug(slug);
            if (Book == null)
                return NotFound();

            Comments = _commentService.GetComments(Book.BookId);
            return Page();
        }

        public IActionResult OnPost(string slug)
        {

            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("Book", new { slug });

            if(!ModelState.IsValid)
            {
                Book = _booksService.GetBookBySlug(slug);
                Comments = _commentService.GetComments(Book.BookId);

                return Page();
            }

            var currentUserId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            _commentService.CreateComment(new CreateCommentDto()
            {
                BookId = BookId,
                Text = Text,
                UserId = currentUserId
            });
            return RedirectToPage("Book", new { slug });


        }
    }
}
