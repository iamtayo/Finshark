using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinsharkApi.DTOs.StockDTOs.CommentDTOs;
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
        private readonly IStockRepository _stoRepo;
        public CommentController(ICommentRepository comRepo, IStockRepository stoRepo)
        {
            _comRepo = comRepo;
            _stoRepo = stoRepo;
        }

        [HttpGet("get-comments")]

        public async Task<IActionResult> GetAllComments()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var comments = await _comRepo.GetAllComments();
            var commentDTOs = comments.Select(c => c.ToCommentDTO()).ToList();
            return Ok(commentDTOs);
        }

        [HttpGet("get-comment/{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comment = await _comRepo.GetCommentById(id);
            if (comment == null)
            {
                return NotFound(comment);
            }
            return Ok(comment.ToCommentDTO());
        }


        [HttpPost("add-comment{stockId:int}")]
        public async Task<IActionResult> AddComment([FromRoute] int stockId, CreateCommentDTO commentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!await _stoRepo.StockExistsAsync(stockId))
            {
                return BadRequest("Stock does not exist");
            }

            var commentModel = commentDTO.ToCommentCreate();
            var createdComment = await _comRepo.CreateComment(commentModel);
            return CreatedAtAction(nameof(GetCommentById), new { id = createdComment.Id }, createdComment.ToCommentDTO());
        }

        [HttpPut("update-comment/{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, UpdateCommentDTO commentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comment = await _comRepo.UpdateComment(id, commentDTO.ToCommentUpdate());
            if (comment == null)
            {
                return NotFound("Comment not found");
            }
            return Ok(comment.ToCommentDTO());
        }

        [HttpDelete("delete-comment/{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comment = await _comRepo.DeleteComment(id);
            if (comment == null)
            {
                return NotFound("Comment not found");
            }
            return Ok(comment.ToCommentDTO());
        }
    }
}