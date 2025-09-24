using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinsharkApi.Data;
using FinsharkApi.DTOs;
using FinsharkApi.Interfaces;
using FinsharkApi.Mappers;
using FinsharkApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FinsharkApi.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Stock>> GetAllStocksAsync(Helpers.QueryObject query)
        {
            var stocks  =  _context.Stocks.Include(c => c.Comments).AsQueryable();

            if (!string.IsNullOrEmpty(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol == query.Symbol);
            }

            if (!string.IsNullOrEmpty(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName == query.CompanyName);
            }
            if(!string.IsNullOrEmpty(query.SortBy))
            {
                switch (query.SortBy.ToLower())
                {
                    case "symbol":
                        stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                        break;
                    case "companyname":
                        stocks = query.IsDescending ? stocks.OrderByDescending(s => s.CompanyName) : stocks.OrderBy(s => s.CompanyName);
                        break;
                    case "price":
                        stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Price) : stocks.OrderBy(s => s.Price);
                        break;
                    case "marketcap":
                        stocks = query.IsDescending ? stocks.OrderByDescending(s => s.MarketCap) : stocks.OrderBy(s => s.MarketCap);
                        break;
                    default:
                        break;
                }
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetStockByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
        } 
        public async Task<Stock> CreateStockAsync(Stock stock)
        {
           
            
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }
        public async Task<Stock?> UpdateStockAsync(int id, UpdateStockDTO dto)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (existingStock == null)
            {
                return null;
            }
            existingStock.Symbol = dto.Symbol;
            existingStock.CompanyName = dto.CompanyName;
            existingStock.Price = dto.Price;
            existingStock.LastDiv = dto.LastDiv;
            existingStock.Industry = dto.Industry;
            existingStock.MarketCap = dto.MarketCap;

            await _context.SaveChangesAsync();
            return existingStock;
        }
        public async Task<Stock?> DeleteStockAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null)
            {
                return null;
            }
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public Task<bool> StockExistsAsync(int id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == id);
        }
    }
}