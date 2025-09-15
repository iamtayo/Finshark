using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinsharkApi.Models;

namespace FinsharkApi.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllComments();
        Task<Comment?> GetCommentById(int id);
        
    }
}