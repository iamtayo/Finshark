using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinsharkApi.Data;
using FinsharkApi.DTOs;
using FinsharkApi.Interfaces;
using FinsharkApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace FinsharkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;

        public StockController( IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }
        [HttpGet("get-stocks")]
        public async Task<IActionResult> GetStocks([FromQuery] Helpers.QueryObject query)
        {
            var stocks = await _stockRepo.GetAllStocksAsync(query);
            return Ok(stocks);
        }

        [HttpGet("get-stock/{id:int}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stock = await _stockRepo.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }

        [HttpPost]
        [Route("create-stock")]
        public async Task<IActionResult> Create([FromBody] CreateStockDTO stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockModel = stockDto.ToStockFromCreateDTO();
              await _stockRepo.CreateStockAsync(stockModel);
                return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, stockModel);
            

        }

        [HttpPut]
        [Route("update-stock/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stock = await _stockRepo.UpdateStockAsync(id, dto);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }

        [HttpDelete]
        [Route("delete-stock/{id:int}")]

        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stock = await _stockRepo.DeleteStockAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return NoContent();

        }
    }
}