using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinsharkApi.Interfaces;
using FinsharkApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace FinsharkApi.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _comRepo;
        public CommentController(ICommentRepository comRepo)
        {
            _comRepo = comRepo;
        }

        [HttpGet("get-comments")]

        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _comRepo.GetAllComments();
            var commentDTOs = comments.Select(c => c.ToCommentDTO()).ToList();
            return Ok(commentDTOs);
        }

        [HttpGet("get-comment/{id}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            var comment = await _comRepo.GetCommentById(id);
            if (comment == null)
            {
                return NotFound(comment);
            }
            return Ok(comment.ToCommentDTO());
        }

    }
}