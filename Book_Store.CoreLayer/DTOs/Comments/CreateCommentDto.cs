namespace Book_Store.CoreLayer.DTOs.Comments
{
    public class CreateCommentDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Text { get; set; }
    }
}
