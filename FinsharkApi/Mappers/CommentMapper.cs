using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinsharkApi.DTOs.StockDTOs.CommentDTOs;
using FinsharkApi.Models;


namespace FinsharkApi.Mappers
{
    public static class CommentMapper
    {
        public static CommentDTO ToCommentDTO(this Comment commentModel) {
            return new CommentDTO
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedAt = commentModel.CreatedAt,
                StockId = commentModel.StockId
            };
        }
        public static Comment ToCommentCreate(this CreateCommentDTO commentDto) {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
            };
          
        }
        public static Comment ToCommentUpdate(this UpdateCommentDTO commentDto) {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
            };
          
        }
    }
}