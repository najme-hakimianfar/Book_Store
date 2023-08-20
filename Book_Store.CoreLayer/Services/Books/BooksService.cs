using Book_Store.CoreLayer.DTOs.Books;
using Book_Store.CoreLayer.Mappers;
using Book_Store.CoreLayer.Services.FileManager;
using Book_Store.CoreLayer.Utilities;
using Book_Store.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Book_Store.CoreLayer.Services.Books
{
    public class BooksService : IBooksService
    {
        private readonly DataBaseContext _context;
        public readonly IFileManager _fileManager;

        public BooksService(DataBaseContext context, IFileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }
        public OperationResult CreateBook(CreateBookDto command)
        {
            var isSlugExist= _context.Books.Any(p => p.Slug == command.Slug.ToSlug());
            if (isSlugExist)
                return OperationResult.Error("این slug وجود دارد");

            if (command.ImageFile == null)
                return OperationResult.Error();

           
            var book = BookMapper.MapCreateDtoToBook(command);

            book.ImageName = _fileManager.SaveFileAndReturnName(command.ImageFile, Directories.BookImage);
            _context.Books.Add(book);
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult EditBook(EditBookDto command)
        {
            var book = _context.Books.FirstOrDefault(c => c.Id == command.BookId);

            if (book == null)
                return OperationResult.NotFound();

            var newSlug = command.Slug.ToSlug();
            var isSlugExist = _context.Books.Any(p => p.Slug == command.Slug.ToSlug());

            if (book.Slug != newSlug)
                if (isSlugExist)
                    return OperationResult.Error("این slug وجود دارد");

            BookMapper.EditDtoToBook(command, book);

            if (command.ImageFile != null)
                book.ImageName = _fileManager.SaveFileAndReturnName(command.ImageFile,Directories.BookImage);
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public BookFilterDto GetBookByFilter(int pageId, int take, string name, string categorySlug)
        {
            var result = _context.Books.Include(d => d.Category)
                .Include(d => d.SubCategory)
                .OrderByDescending(d => d.CreationDate)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(categorySlug))
                result = result.Where(r => r.Category.Slug == categorySlug
                    || r.SubCategory.Slug ==categorySlug);

            if (!string.IsNullOrWhiteSpace(name))
                result = result.Where(r => r.Name.Contains(name));

            var skip = (pageId - 1) * take;
            var pageCount = (int)Math.Ceiling(result.Count() / (double)take);
            var model= new BookFilterDto()
            {
                Books = result.Skip(skip).Take(take)
                    .Select(book => BookMapper.MapToDto(book)).ToList(),
                PageId=pageId,
                Take=take,
                Name=name,
                CategorySlug=categorySlug
            };
            model.GeneratePaging(result,take,pageId);
            return model;
        }

        public BookDto GetBookById(int bookId)
        {
            var book = _context.Books
                .Include(c=> c.SubCategory)
                .Include(c=> c.Category)
                .FirstOrDefault(c => c.Id == bookId);
            return BookMapper.MapToDto(book);
        }

        public BookDto GetBookBySlug(string slug)
        {
            var book = _context.Books
                .Include(c => c.SubCategory)
                .Include(c => c.Category)
                .FirstOrDefault(c => c.Slug == slug);
            if (book == null)
                return null;
            return BookMapper.MapToDto(book);
        }

        public OperationResult DeleteBook(int bookId)
        {
            var book = _context.Books.FirstOrDefault(c => c.Id == bookId);

            if (book == null)
                return OperationResult.NotFound();

            book.IsDelete = true;
            _context.SaveChanges();
            return OperationResult.Success();
        }

    }
}
