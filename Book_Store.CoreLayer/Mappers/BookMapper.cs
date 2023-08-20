using Book_Store.CoreLayer.DTOs.Books;
using Book_Store.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.Mappers
{
    public static class BookMapper
    {
        public static Book MapCreateDtoToBook(CreateBookDto dto)
        {
            return new Book()
            {
                Name=dto.Name,
                CategoryId=dto.CategoryId,
                Slug=dto.Slug,
                Publisher=dto.Publisher,
                Translator=dto.Translator,
                Writer=dto.Writer,
                PYear=dto.PYear,
                Price=dto.Price,
                NumberPage=dto.NumberPage,
                Size=dto.Size,
                CoverType=dto.CoverType,
                Weight=dto.Weight,
                IsDelete=false,
                SubCategoryId=dto.SubCategoryId,
                IsSpecial=dto.IsSpecial,
                Discount=dto.Discount
                
            };
        }

        public static BookDto MapToDto(Book book)
        {

            return new BookDto()
            {
                Name = book.Name,
                CategoryId = book.CategoryId,
                Slug = book.Slug,
                Publisher = book.Publisher,
                Translator = book.Translator,
                Writer = book.Writer,
                PYear = book.PYear,
                Price = book.Price,
                NumberPage = book.NumberPage,
                Size = book.Size,
                CoverType = book.CoverType,
                Weight = book.Weight,
                CreationDate = book.CreationDate,
                Category = CategoryMapper.Map(book.Category),
                ImageName=book.ImageName,
                BookId=book.Id,
                SubCategoryId=book.SubCategoryId,
                //اگر زیر دسته نال نبود 
                SubCategory=book.SubCategoryId==null?null:CategoryMapper.Map(book.SubCategory),
                IsSpecial=book.IsSpecial,
                Discount=book.Discount   
            };
        }

        public static Book EditDtoToBook(EditBookDto editDto,Book book)
        {
            book.Name = editDto.Name;
            book.CategoryId = editDto.CategoryId;
            book.Slug = editDto.Slug;
            book.Publisher = editDto.Publisher;
            book.Translator = editDto.Translator;
            book.Writer = editDto.Writer;
            book.PYear = editDto.PYear;
            book.Price = editDto.Price;
            book.NumberPage = editDto.NumberPage;
            book.Size = editDto.Size;
            book.CoverType = editDto.CoverType;
            book.Weight = editDto.Weight;
            book.SubCategoryId = editDto.SubCategoryId;
            book.IsSpecial = editDto.IsSpecial;
            book.Discount = editDto.Discount;
            return book;
        }
    }
}
