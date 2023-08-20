﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.CoreLayer.DTOs.Comments
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string UserFullName { get; set; }
        public int BookId { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
