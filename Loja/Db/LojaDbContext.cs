using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Loja.Domain;
using Loja.MigrationUtils;

namespace Loja.Db;

public class LojaDbContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; }

    public DbSet<Produto> Produtos { get; set; }

    public DbSet<Pedido> Pedidos { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<Endereco> Enderecos { get; set; }

    public DbSet<UF> UFs { get; set; }

    public DbSet<Preferencia> Preferencias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /* 
         * Na linha comentada abaixo eu criei um serviço para gerar as triggers de 
         * atualização do campo UpdatedAt da tabelas, mas está na sintaxe do SQLite.
         * Se rodar no PostgreSQL deve dar pau, então eu comentei.
         * Não sei se a gente vai usar isso no sistema da Go Bee.
         */

        optionsBuilder
            .UseLazyLoadingProxies()
            .ReplaceService<IMigrationsModelDiffer, InsertSqlCommands>()
            .UseSqlite("Data Source=c:\\projetos\\CSharp\\Loja\\LojaConsole.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		// USAR ESSE COMANDO PARA SUBSTITUIR OS DEMAIS!
		// modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext));
		
        new ProdutoConfiguration().Configure(modelBuilder.Entity<Produto>());
        new ClienteConfiguration().Configure(modelBuilder.Entity<Cliente>());
        new ItemConfiguration().Configure(modelBuilder.Entity<Item>());
        new PedidoConfiguration().Configure(modelBuilder.Entity<Pedido>());
        new EnderecoConfiguration().Configure(modelBuilder.Entity<Endereco>());
        new UFConfiguration().Configure(modelBuilder.Entity<UF>());
        new PreferenciaConfiguration().Configure(modelBuilder.Entity<Preferencia>());

    }
}
