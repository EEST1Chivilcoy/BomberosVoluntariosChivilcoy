using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHistorialCuotaToSocios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistorialSocios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaDeCambio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SocioId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "varchar(34)", maxLength: 34, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FrecuenciaDePagoAnterior = table.Column<int>(type: "int", nullable: true),
                    FormaDePagoAnterior = table.Column<int>(type: "int", nullable: true),
                    MontoAnterior = table.Column<double>(type: "double", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EstadoAnterior = table.Column<int>(type: "int", nullable: true),
                    Motivo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialSocios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialSocios_Socios_SocioId",
                        column: x => x.SocioId,
                        principalTable: "Socios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialSocios_SocioId",
                table: "HistorialSocios",
                column: "SocioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorialSocios");
        }
    }
}
