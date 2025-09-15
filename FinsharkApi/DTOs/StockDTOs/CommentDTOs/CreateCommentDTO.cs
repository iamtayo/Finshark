using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinsharkApi.DTOs.StockDTOs.CommentDTOs
{
    public class CreateCommentDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

    }
}