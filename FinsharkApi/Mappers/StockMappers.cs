using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinsharkApi.DTOs;
using FinsharkApi.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FinsharkApi.Mappers
{
    public static class StockMappers
    {
        public static Stock ToStockFromCreateDTO(this CreateStockDTO dto)
        {
            return new Stock
            {
                Symbol = dto.Symbol,
                CompanyName = dto.CompanyName,
                Price = dto.Price,
                LastDiv = dto.LastDiv,
                Industry = dto.Industry,
                MarketCap = dto.MarketCap
            };
        }
        public static CreateStockDTO ToStockDTO(this Stock stock)
        {
            return new CreateStockDTO
            {
        Symbol = stock.Symbol,
        CompanyName = stock.CompanyName,
        Price = stock.Price,
        LastDiv = stock.LastDiv,
        Industry = stock.Industry,
        MarketCap = stock.MarketCap
    };
        }
    }
}