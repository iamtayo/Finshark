using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinsharkApi.Data;
using FinsharkApi.DTOs;
using FinsharkApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace FinsharkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public StockController(ApplicationDBContext context)
        {
            _context = context;

        }
        [HttpGet("get-stocks")]


        public IActionResult GetStocks()
        {
            var stocks = _context.Stocks.ToList();
            return Ok(stocks);
        }

        [HttpGet("get-stock/{id}")]
        public IActionResult GetStockById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }

        [HttpPost]
        [Route("create-stock")]
        public IActionResult Create([FromBody] CreateStockDTO dto)
        {
            var stock = dto.ToStockFromCreateDTO();
            {
                if (stock == null)
                {
                    return BadRequest();
                }
                _context.Stocks.Add(stock);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetStockById), new { id = stock.Id }, stock);
            }

        }

        [HttpPut]
        [Route("update-stock/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockDTO dto)
        {
            var stock = _context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stock == null)
            {
                return NotFound();
            }

            stock.Symbol = dto.Symbol;
            stock.CompanyName = dto.CompanyName;
            stock.Price = dto.Price;
            stock.LastDiv = dto.LastDiv;
            stock.Industry = dto.Industry;
            stock.MarketCap = dto.MarketCap;

            _context.SaveChanges();
            return Ok(stock);
        }

        [HttpDelete]
        [Route("delete-stock/{id}")]

        public IActionResult DeleteStock([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stock);
            _context.SaveChanges();
            return NoContent();

        }
    }
}