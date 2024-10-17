using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTO
{
    public class ProdutoCreateDto
    {
         public required string Name { get; set; } 
    public decimal Price { get; set; }
    public required string Description { get; set; }
    public int Quantity { get; set; }
    public required string Type { get; set; } 
    }
}