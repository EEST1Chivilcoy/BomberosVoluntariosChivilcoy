using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSociosAndCobradores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cobrador_Estado",
                table: "Persona",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZonasAsignadas",
                table: "Persona",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Socios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EstadoSocio = table.Column<int>(type: "int", nullable: false),
                    MontoCuota = table.Column<double>(type: "double", nullable: false),
                    FrecuenciaDePago = table.Column<int>(type: "int", nullable: false),
                    DocumentoOCUIT = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Apellido = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Localidad = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitud = table.Column<double>(type: "double", nullable: true),
                    Longitud = table.Column<double>(type: "double", nullable: true),
                    Zona = table.Column<int>(type: "int", nullable: false),
                    Ocupacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefono = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socios", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HistorialesSocios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaDeCambio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SocioId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "varchar(21)", maxLength: 21, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FrecuenciaDePagoAnterior = table.Column<int>(type: "int", nullable: true),
                    MontoAnterior = table.Column<double>(type: "double", nullable: true),
                    FrecuenciaDePagoNueva = table.Column<int>(type: "int", nullable: true),
                    MontoNuevo = table.Column<double>(type: "double", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EstadoAnterior = table.Column<int>(type: "int", nullable: true),
                    EstadoNuevo = table.Column<int>(type: "int", nullable: true),
                    Motivo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FormaDePago = table.Column<int>(type: "int", nullable: true),
                    CobradorQueCobroPersonaId = table.Column<int>(type: "int", nullable: true),
                    Monto = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialesSocios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialesSocios_Persona_CobradorQueCobroPersonaId",
                        column: x => x.CobradorQueCobroPersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistorialesSocios_Socios_SocioId",
                        column: x => x.SocioId,
                        principalTable: "Socios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialesSocios_CobradorQueCobroPersonaId",
                table: "HistorialesSocios",
                column: "CobradorQueCobroPersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialesSocios_SocioId",
                table: "HistorialesSocios",
                column: "SocioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorialesSocios");

            migrationBuilder.DropTable(
                name: "Socios");

            migrationBuilder.DropColumn(
                name: "Cobrador_Estado",
                table: "Persona");

            migrationBuilder.DropColumn(
                name: "ZonasAsignadas",
                table: "Persona");
        }
    }
}
