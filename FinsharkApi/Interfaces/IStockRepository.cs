using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinsharkApi.DTOs;
using FinsharkApi.Models;

namespace FinsharkApi.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync();
        Task<Stock?> GetStockByIdAsync(int id);
        Task<Stock> CreateStockAsync(Stock stock);
        Task<Stock?> UpdateStockAsync(int id, UpdateStockDTO stock);
        Task<Stock?> DeleteStockAsync(int id);

    }
}