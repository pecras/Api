using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTO;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly ILogger<ProdutosController> _logger;

        public ProdutosController(ApiDbContext context, ILogger<ProdutosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // POST: api/produtos
        [HttpPost]
     public async Task<IActionResult> CriarProduto([FromBody] ProdutoCreateDto produtoDto)
{
    if (produtoDto == null)
    {
        return BadRequest("Produto inválido.");
    }

    var produto = new Produto
    {
        Name = produtoDto.Name,
        Price = produtoDto.Price,
        Description = produtoDto.Description,
        Quantity = produtoDto.Quantity,
        Type = produtoDto.Type,
        Date = DateTime.Now  // Define a data de cadastro
    };

    _context.Produtos.Add(produto);      // Adiciona o produto ao contexto do banco de dados
    await _context.SaveChangesAsync();   // Salva as alterações no banco

    return Ok(produto); // Retorna o produto criado
}

        // GET: api/produtos
        [HttpGet]
        public IActionResult ListarProdutos([FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 5)
        {
            var produtosPaginados = _context.Produtos
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToList();

            return Ok(produtosPaginados);  // Retorna a lista de produtos paginada
        }


[HttpGet("All")]
public IActionResult ListarTodosProdutos()
{
    var todosProdutos = _context.Produtos.ToList();
    return Ok(todosProdutos);  // Retorna todos os produtos sem filtro
}



         // PUT: api/produtos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProduto(int id, [FromBody] Produto produtoAtualizado)
        {
            if (produtoAtualizado == null || id != produtoAtualizado.Id)
            {
                return BadRequest("Dados inválidos.");
            }

            var produtoExistente = await _context.Produtos.FindAsync(id);
            if (produtoExistente == null)
            {
                return NotFound("Produto não encontrado.");
            }

           
            produtoExistente.Name = produtoAtualizado.Name;
            produtoExistente.Description = produtoAtualizado.Description;
            produtoExistente.Price=produtoAtualizado.Price;
            produtoExistente.Quantity=produtoAtualizado.Quantity;
            produtoExistente.Type=produtoAtualizado.Type;
            produtoExistente.Date=produtoAtualizado.Date;

            try
            {
                await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
                return Ok(produtoExistente); // Retorna o produto atualizado
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar o produto: {ex.Message}");
                return StatusCode(500, "Erro ao atualizar o produto.");
            }
        }

        // DELETE: api/produtos/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> ExcluirProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }

            _context.Produtos.Remove(produto); // Remove o produto do banco de dados

            try
            {
                await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao excluir o produto: {ex.Message}");
                return StatusCode(500, "Erro ao excluir o produto.");
            }
        }
    
    }
}
