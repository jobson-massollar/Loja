using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loja.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Preferencias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()"),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferencias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CodigoBarras_Valor = table.Column<string>(type: "TEXT", nullable: false),
                    Preco_Moeda = table.Column<string>(type: "TEXT", nullable: false),
                    Preco_Valor = table.Column<float>(type: "REAL", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()"),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UFs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Sigla = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()"),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UFs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Logradouro = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Numero = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Complemento = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Bairro = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    UFId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Cep_Valor = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()"),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enderecos_UFs_UFId",
                        column: x => x.UFId,
                        principalTable: "UFs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CPF_Valor = table.Column<long>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    EnderecoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Telefone_DDD = table.Column<int>(type: "INTEGER", nullable: true),
                    Telefone_Numero = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()"),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientePreferencia",
                columns: table => new
                {
                    ClientesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PreferenciasId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientePreferencia", x => new { x.ClientesId, x.PreferenciasId });
                    table.ForeignKey(
                        name: "FK_ClientePreferencia_Clientes_ClientesId",
                        column: x => x.ClientesId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientePreferencia_Preferencias_PreferenciasId",
                        column: x => x.PreferenciasId,
                        principalTable: "Preferencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClienteId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EnderecoEntregaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()"),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Enderecos_EnderecoEntregaId",
                        column: x => x.EnderecoEntregaId,
                        principalTable: "Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Itens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    PedidoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Preco_Moeda = table.Column<string>(type: "TEXT", nullable: false),
                    Preco_Valor = table.Column<float>(type: "REAL", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()"),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Itens_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Itens_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientePreferencia_PreferenciasId",
                table: "ClientePreferencia",
                column: "PreferenciasId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CPF_Valor",
                table: "Clientes",
                column: "CPF_Valor",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Email",
                table: "Clientes",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EnderecoId",
                table: "Clientes",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_UFId",
                table: "Enderecos",
                column: "UFId");

            migrationBuilder.CreateIndex(
                name: "IX_Itens_PedidoId",
                table: "Itens",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Itens_ProdutoId",
                table: "Itens",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedidos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_EnderecoEntregaId",
                table: "Pedidos",
                column: "EnderecoEntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_Preferencias_Descricao",
                table: "Preferencias",
                column: "Descricao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UFs_Sigla",
                table: "UFs",
                column: "Sigla",
                unique: true);

            migrationBuilder.Sql("\r\nDROP TRIGGER IF EXISTS Clientes_AfterUpdate;\r\nCREATE TRIGGER IF NOT EXISTS Clientes_AfterUpdate \r\n         AFTER UPDATE\r\n            ON Clientes\r\n          WHEN old.UpdatedAt <> CURRENT_TIMESTAMP\r\nBEGIN\r\n    UPDATE Clientes\r\n       SET UpdatedAt = CURRENT_TIMESTAMP\r\n     WHERE id = OLD.id;\r\nEND;\r\n");

            migrationBuilder.Sql("\r\nDROP TRIGGER IF EXISTS Enderecos_AfterUpdate;\r\nCREATE TRIGGER IF NOT EXISTS Enderecos_AfterUpdate \r\n         AFTER UPDATE\r\n            ON Enderecos\r\n          WHEN old.UpdatedAt <> CURRENT_TIMESTAMP\r\nBEGIN\r\n    UPDATE Enderecos\r\n       SET UpdatedAt = CURRENT_TIMESTAMP\r\n     WHERE id = OLD.id;\r\nEND;\r\n");

            migrationBuilder.Sql("\r\nDROP TRIGGER IF EXISTS Itens_AfterUpdate;\r\nCREATE TRIGGER IF NOT EXISTS Itens_AfterUpdate \r\n         AFTER UPDATE\r\n            ON Itens\r\n          WHEN old.UpdatedAt <> CURRENT_TIMESTAMP\r\nBEGIN\r\n    UPDATE Itens\r\n       SET UpdatedAt = CURRENT_TIMESTAMP\r\n     WHERE id = OLD.id;\r\nEND;\r\n");

            migrationBuilder.Sql("\r\nDROP TRIGGER IF EXISTS Pedidos_AfterUpdate;\r\nCREATE TRIGGER IF NOT EXISTS Pedidos_AfterUpdate \r\n         AFTER UPDATE\r\n            ON Pedidos\r\n          WHEN old.UpdatedAt <> CURRENT_TIMESTAMP\r\nBEGIN\r\n    UPDATE Pedidos\r\n       SET UpdatedAt = CURRENT_TIMESTAMP\r\n     WHERE id = OLD.id;\r\nEND;\r\n");

            migrationBuilder.Sql("\r\nDROP TRIGGER IF EXISTS Preferencias_AfterUpdate;\r\nCREATE TRIGGER IF NOT EXISTS Preferencias_AfterUpdate \r\n         AFTER UPDATE\r\n            ON Preferencias\r\n          WHEN old.UpdatedAt <> CURRENT_TIMESTAMP\r\nBEGIN\r\n    UPDATE Preferencias\r\n       SET UpdatedAt = CURRENT_TIMESTAMP\r\n     WHERE id = OLD.id;\r\nEND;\r\n");

            migrationBuilder.Sql("\r\nDROP TRIGGER IF EXISTS Produtos_AfterUpdate;\r\nCREATE TRIGGER IF NOT EXISTS Produtos_AfterUpdate \r\n         AFTER UPDATE\r\n            ON Produtos\r\n          WHEN old.UpdatedAt <> CURRENT_TIMESTAMP\r\nBEGIN\r\n    UPDATE Produtos\r\n       SET UpdatedAt = CURRENT_TIMESTAMP\r\n     WHERE id = OLD.id;\r\nEND;\r\n");

            migrationBuilder.Sql("\r\nDROP TRIGGER IF EXISTS UFs_AfterUpdate;\r\nCREATE TRIGGER IF NOT EXISTS UFs_AfterUpdate \r\n         AFTER UPDATE\r\n            ON UFs\r\n          WHEN old.UpdatedAt <> CURRENT_TIMESTAMP\r\nBEGIN\r\n    UPDATE UFs\r\n       SET UpdatedAt = CURRENT_TIMESTAMP\r\n     WHERE id = OLD.id;\r\nEND;\r\n");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientePreferencia");

            migrationBuilder.DropTable(
                name: "Itens");

            migrationBuilder.DropTable(
                name: "Preferencias");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "UFs");
        }
    }
}
