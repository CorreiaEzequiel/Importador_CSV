using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeradorArquivos.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotasFiscais_Clientes_ClienteId",
                table: "NotasFiscais");

            migrationBuilder.DropForeignKey(
                name: "FK_NotasFiscais_Produtos_ProdutoId",
                table: "NotasFiscais");

            migrationBuilder.DropIndex(
                name: "IX_NotasFiscais_ClienteId",
                table: "NotasFiscais");

            migrationBuilder.DropIndex(
                name: "IX_NotasFiscais_ProdutoId",
                table: "NotasFiscais");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "NotasFiscais");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "NotasFiscais");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "NotasFiscais",
                newName: "DataEmissao");

            migrationBuilder.CreateTable(
                name: "NotaFiscalProdutos",
                columns: table => new
                {
                    NotaFiscalId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaFiscalProdutos", x => new { x.NotaFiscalId, x.ProdutoId });
                    table.ForeignKey(
                        name: "FK_NotaFiscalProdutos_NotasFiscais_NotaFiscalId",
                        column: x => x.NotaFiscalId,
                        principalTable: "NotasFiscais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotaFiscalProdutos_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscalProdutos_ProdutoId",
                table: "NotaFiscalProdutos",
                column: "ProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotaFiscalProdutos");

            migrationBuilder.RenameColumn(
                name: "DataEmissao",
                table: "NotasFiscais",
                newName: "Data");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "NotasFiscais",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "NotasFiscais",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_ClienteId",
                table: "NotasFiscais",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_ProdutoId",
                table: "NotasFiscais",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotasFiscais_Clientes_ClienteId",
                table: "NotasFiscais",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotasFiscais_Produtos_ProdutoId",
                table: "NotasFiscais",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
