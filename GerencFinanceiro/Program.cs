using GerencFinanceiro.Dal;
using GerencFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner
builder.Services.AddControllersWithViews(); // Alterado para suportar Views
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionar o AppDbContext e a conexão com PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurando a injeção de dependência para IFinancasDAL e FinancasDAL
builder.Services.AddTransient<IFinancasDAL, FinancasDAL>();

// Configurando o log
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();   // Exibe logs no console
    logging.AddDebug();     // Exibe logs de debug
});

// Construir a aplicação
var app = builder.Build();

// Configurando o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Exibe detalhes de exceções no modo de desenvolvimento
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Exibe uma página genérica de erro em produção
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Adicionado
app.UseRouting();     // Adicionado
app.UseAuthorization();

// Definindo as rotas do MVC
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Financas}/{action=Index}/{id?}");
});

// Executar a aplicação
app.Run();
