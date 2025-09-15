using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinsharkApi.DTOs.StockDTOs.CommentDTOs
{
    public class UpdateCommentDTO
    {
         public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}