using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class EquiposAutonomos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquiposAutonomos",
                columns: table => new
                {
                    EquipoAutonomoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NroSerie = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NroTubo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Marca = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Modelo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoMaterial = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquiposAutonomos", x => x.EquipoAutonomoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MovimientosEquiposAutonomos",
                columns: table => new
                {
                    Movimiento_EquipoAutonomoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EquipoAutonomoId = table.Column<int>(type: "int", nullable: false),
                    FechaMovimiento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EncargadoId = table.Column<int>(type: "int", nullable: false),
                    EstadoAnterior = table.Column<int>(type: "int", nullable: false),
                    EstadoNuevo = table.Column<int>(type: "int", nullable: false),
                    VehiculoAgenteVehiculoId = table.Column<int>(type: "int", nullable: true),
                    DependenciaAgenteDependenciaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosEquiposAutonomos", x => x.Movimiento_EquipoAutonomoId);
                    table.ForeignKey(
                        name: "FK_MovimientosEquiposAutonomos_Dependencias_DependenciaAgenteDe~",
                        column: x => x.DependenciaAgenteDependenciaId,
                        principalTable: "Dependencias",
                        principalColumn: "DependenciaId");
                    table.ForeignKey(
                        name: "FK_MovimientosEquiposAutonomos_EquiposAutonomos_EquipoAutonomoId",
                        column: x => x.EquipoAutonomoId,
                        principalTable: "EquiposAutonomos",
                        principalColumn: "EquipoAutonomoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimientosEquiposAutonomos_Persona_EncargadoId",
                        column: x => x.EncargadoId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimientosEquiposAutonomos_Vehiculo_VehiculoAgenteVehiculoId",
                        column: x => x.VehiculoAgenteVehiculoId,
                        principalTable: "Vehiculo",
                        principalColumn: "VehiculoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosEquiposAutonomos_DependenciaAgenteDependenciaId",
                table: "MovimientosEquiposAutonomos",
                column: "DependenciaAgenteDependenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosEquiposAutonomos_EncargadoId",
                table: "MovimientosEquiposAutonomos",
                column: "EncargadoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosEquiposAutonomos_EquipoAutonomoId",
                table: "MovimientosEquiposAutonomos",
                column: "EquipoAutonomoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosEquiposAutonomos_VehiculoAgenteVehiculoId",
                table: "MovimientosEquiposAutonomos",
                column: "VehiculoAgenteVehiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientosEquiposAutonomos");

            migrationBuilder.DropTable(
                name: "EquiposAutonomos");
        }
    }
}
