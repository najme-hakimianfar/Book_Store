using Book_Store.CoreLayer.DTOs.Comments;
using Book_Store.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.Services.Comments
{
    public interface ICommentService
    {
        OperationResult CreateComment(CreateCommentDto command);
        List<CommentDto> GetComments(int bookId);
    }
}
