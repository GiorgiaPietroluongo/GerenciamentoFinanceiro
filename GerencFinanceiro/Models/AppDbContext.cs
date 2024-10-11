using Microsoft.EntityFrameworkCore;

namespace GerencFinanceiro.Models
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<RelatorioDespesa> RelatorioDespesas { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações de modelo para RelatorioDespesa
            modelBuilder.Entity<RelatorioDespesa>(entity =>
            {
                entity.HasKey(e => e.ItemId); // Configurando chave primária

                entity.Property(e => e.ItemNome)
                      .IsRequired()
                      .HasMaxLength(200); // Configurando nome da despesa com tamanho máximo

                entity.Property(e => e.Valor)
                      .HasColumnType("numeric(10,2)"); // Tipo de dado adequado para PostgreSQL

                entity.Property(e => e.Date)
                      .HasColumnType("date"); // Garantindo que seja salvo apenas a data no PostgreSQL

                entity.Property(e => e.Categoria)
                      .IsRequired()
                      .HasMaxLength(100); // Configuração para Categoria
            });
        }
    }
}
