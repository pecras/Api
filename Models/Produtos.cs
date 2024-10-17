using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Produto
    {
    public int Id { get; set; } // O Entity Framework entende que esse campo será uma chave primária auto-incrementada.
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public required string Description { get; set; }
        public int Quantity { get; set; }
        public required string Type { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}