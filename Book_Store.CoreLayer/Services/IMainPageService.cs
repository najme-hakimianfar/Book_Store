using Book_Store.CoreLayer.DTOs;
using Book_Store.CoreLayer.Mappers;
using Book_Store.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.Services
{
    public interface IMainPageService
    {
        MainPageDto GetData();
    }

    public class MainPageService : IMainPageService
    {
        private readonly DataBaseContext _context;
        public MainPageService(DataBaseContext context)
        {
            _context = context;
        }
        public MainPageDto GetData()
        {
            var specialBooks = _context.Books
                .OrderByDescending(d => d.Id)
                .Include(c => c.Category)
                .Include(c => c.SubCategory)
                .Where(r => r.IsSpecial)
                .Take(6)
                .Select(book => BookMapper.MapToDto(book)).ToList();

            var latestBooks= _context.Books
                .OrderByDescending(d => d.Id)
                .Include(c => c.Category)
                .Include(c => c.SubCategory)
                .Take(6)
                .Select(book => BookMapper.MapToDto(book)).ToList();

            var dicountBooks = _context.Books
                .OrderByDescending(d => d.Id)
                .Include(c => c.Category)
                .Include(c => c.SubCategory)
                .Where(r => r.Discount!=null &&r.Discount!=0)
                .Take(6)
                .Select(book => BookMapper.MapToDto(book)).ToList();

            return new MainPageDto()
            {
                LatestBook = latestBooks,
                SpecialBook = specialBooks,
                DiscountBook=dicountBooks
            };
        }
    }
}
