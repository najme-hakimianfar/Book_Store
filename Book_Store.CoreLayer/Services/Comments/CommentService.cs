using Book_Store.CoreLayer.DTOs.Comments;
using Book_Store.CoreLayer.Utilities;
using Book_Store.DataLayer.Context;
using Book_Store.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Book_Store.CoreLayer.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly DataBaseContext _context;
        public CommentService(DataBaseContext context)
        {
            _context = context;
        }

        public OperationResult CreateComment(CreateCommentDto command)
        {
            var comment = new Comment()
            {
                BookId = command.BookId,
                Text = command.Text,
                UserId = command.UserId
            };
            _context.Add(comment);
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public List<CommentDto> GetComments(int bookId)
        {
            return _context.Comments
                .Include(c => c.User)
                .Where(c => c.BookId == bookId)
                .Select(comment => new CommentDto()
                {
                    BookId = comment.BookId,
                    Text = comment.Text,
                    UserFullName = comment.User.FullName,
                    CommentId = comment.Id,
                    CreationDate = comment.CreationDate
                }).ToList();
        }
    }
}
