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
        public async Task<IActionResult> GetStocks()
        {
            var stocks = await _stockRepo.GetAllStocksAsync();
            return Ok(stocks);
        }

        [HttpGet("get-stock/{id}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
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
            var stockModel = stockDto.ToStockFromCreateDTO();
              await _stockRepo.CreateStockAsync(stockModel);
                return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, stockModel);
            

        }

        [HttpPut]
        [Route("update-stock/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDTO dto)
        {
            var stock = await _stockRepo.UpdateStockAsync(id, dto);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }

        [HttpDelete]
        [Route("delete-stock/{id}")]

        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stock = await _stockRepo.DeleteStockAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return NoContent();

        }
    }
}