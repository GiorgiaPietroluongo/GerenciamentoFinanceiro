using GerencFinanceiro.Dal;
using GerencFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // Alterado para suportar Views
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionar o AppDbContext e a conexão com PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurando a injeção de dependência para IFinancasDAL e FinancasDAL
builder.Services.AddTransient<IFinancasDAL, FinancasDAL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
 if (app.Environment.IsDevelopment())
 {
     app.UseSwagger();
     app.UseSwaggerUI();
 }

app.UseHttpsRedirection();
app.UseStaticFiles(); // Adicionado
app.UseRouting();     // Adicionado
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Despesa}/{action=Index}/{id?}");
});

app.Run();
