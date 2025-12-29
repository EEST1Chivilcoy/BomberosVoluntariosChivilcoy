using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPagosToSociosAndFixs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistorialSocios_Socios_SocioId",
                table: "HistorialSocios");

            migrationBuilder.CreateTable(
                name: "PagoSocio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    FechaGeneradoPendiente = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaCobro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaConfirmadoORechazado = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Monto = table.Column<double>(type: "double", nullable: false),
                    SocioId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    ConfirmadoPorPersonaId = table.Column<int>(type: "int", nullable: true),
                    CobradorId = table.Column<int>(type: "int", nullable: true),
                    FechaEntregaAComision = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagoSocio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagoSocio_Persona_CobradorId",
                        column: x => x.CobradorId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PagoSocio_Persona_ConfirmadoPorPersonaId",
                        column: x => x.ConfirmadoPorPersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId");
                    table.ForeignKey(
                        name: "FK_PagoSocio_Socios_SocioId",
                        column: x => x.SocioId,
                        principalTable: "Socios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PagoSocio_CobradorId",
                table: "PagoSocio",
                column: "CobradorId");

            migrationBuilder.CreateIndex(
                name: "IX_PagoSocio_ConfirmadoPorPersonaId",
                table: "PagoSocio",
                column: "ConfirmadoPorPersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagoSocio_SocioId",
                table: "PagoSocio",
                column: "SocioId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistorialSocios_Socios_SocioId",
                table: "HistorialSocios",
                column: "SocioId",
                principalTable: "Socios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistorialSocios_Socios_SocioId",
                table: "HistorialSocios");

            migrationBuilder.DropTable(
                name: "PagoSocio");

            migrationBuilder.AddForeignKey(
                name: "FK_HistorialSocios_Socios_SocioId",
                table: "HistorialSocios",
                column: "SocioId",
                principalTable: "Socios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
