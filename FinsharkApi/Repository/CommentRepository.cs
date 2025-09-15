using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinsharkApi.Data;
using FinsharkApi.Interfaces;
using FinsharkApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinsharkApi.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateComment(Comment comment)
        {
           await _context.Comments.AddAsync(comment);
           await _context.SaveChangesAsync();
           return comment;
        }

        public async Task<Comment?> DeleteComment(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c=> c.Id == id);
            if (comment == null) return null;
             _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
            
        }

        public  async Task<List<Comment>> GetAllComments()
        {
            return await  _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetCommentById(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment?> UpdateComment(int id, Comment comment)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null) return null;

            existingComment.Title = comment.Title;
            existingComment.Content = comment.Content;

            await _context.SaveChangesAsync();
            return existingComment;
        }
    }
}