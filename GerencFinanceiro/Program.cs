using GerencFinanceiro.Dal;
using GerencFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar servi�os ao cont�iner
builder.Services.AddControllersWithViews(); // Alterado para suportar Views
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionar o AppDbContext e a conex�o com PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurando a inje��o de depend�ncia para IFinancasDAL e FinancasDAL
builder.Services.AddTransient<IFinancasDAL, FinancasDAL>();

// Configurando o log
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();   // Exibe logs no console
    logging.AddDebug();     // Exibe logs de debug
});

// Construir a aplica��o
var app = builder.Build();

// Configurando o pipeline de requisi��es HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Exibe detalhes de exce��es no modo de desenvolvimento
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Exibe uma p�gina gen�rica de erro em produ��o
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

// Executar a aplica��o
app.Run();
