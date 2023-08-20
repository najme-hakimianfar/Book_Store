using Book_Store.CoreLayer.DTOs.Books;
using Book_Store.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.Services.Books
{
    public interface IBooksService
    {
        OperationResult CreateBook(CreateBookDto command);
        OperationResult EditBook(EditBookDto command);
        BookDto GetBookById(int postId);
        BookDto GetBookBySlug(string slug);
        BookFilterDto GetBookByFilter(int pageId,int take,string name,string categorySlug);
        OperationResult DeleteBook(int bookId);
    }
}
