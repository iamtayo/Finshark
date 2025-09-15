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
        public  async Task<List<Comment>> GetAllComments()
        {
            return await  _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetCommentById(int id)
        {
            return await _context.Comments.FindAsync(id);
        }
    }
}