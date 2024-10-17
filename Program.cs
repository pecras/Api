using Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar o serviço de banco de dados com SQLite
var connect = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiDbContext>(options => options.UseSqlite(connect));

builder.Services.AddControllers();

// Configurar o Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS para liberar todas as origens
builder.Services.AddCors(options =>
{
    options.AddPolicy("liberty", policy =>
    {
        policy.AllowAnyOrigin()    
              .AllowAnyHeader()    
              .AllowAnyMethod();  
    });
});

var app = builder.Build();

// Configurar o pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.UseCors("liberty");

app.MapControllers();

app.Run();



public class ApiDbContext : DbContext
{
    public virtual DbSet<Produto> Produtos { get; set; }

    public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
    {
    }
}
