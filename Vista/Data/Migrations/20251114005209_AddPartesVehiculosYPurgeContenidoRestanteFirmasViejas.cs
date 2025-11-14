using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPartesVehiculosYPurgeContenidoRestanteFirmasViejas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Firmas");

            migrationBuilder.AddColumn<int>(
                name: "LicenciaId",
                table: "Imagen",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PartesVehiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartesVehiculo", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Imagen_LicenciaId",
                table: "Imagen",
                column: "LicenciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Imagen_Licencias_LicenciaId",
                table: "Imagen",
                column: "LicenciaId",
                principalTable: "Licencias",
                principalColumn: "LicenciaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Imagen_Licencias_LicenciaId",
                table: "Imagen");

            migrationBuilder.DropTable(
                name: "PartesVehiculo");

            migrationBuilder.DropIndex(
                name: "IX_Imagen_LicenciaId",
                table: "Imagen");

            migrationBuilder.DropColumn(
                name: "LicenciaId",
                table: "Imagen");

            migrationBuilder.CreateTable(
                name: "Firmas",
                columns: table => new
                {
                    FirmaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    VehiculoId = table.Column<int>(type: "int", nullable: false),
                    Detalle = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmbarcacionVehiculoId = table.Column<int>(type: "int", nullable: true),
                    FechaHora = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firmas", x => x.FirmaId);
                    table.ForeignKey(
                        name: "FK_Firmas_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Firmas_Vehiculo_EmbarcacionVehiculoId",
                        column: x => x.EmbarcacionVehiculoId,
                        principalTable: "Vehiculo",
                        principalColumn: "VehiculoId");
                    table.ForeignKey(
                        name: "FK_Firmas_Vehiculo_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculo",
                        principalColumn: "VehiculoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Firmas_EmbarcacionVehiculoId",
                table: "Firmas",
                column: "EmbarcacionVehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Firmas_PersonaId",
                table: "Firmas",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Firmas_VehiculoId",
                table: "Firmas",
                column: "VehiculoId");
        }
    }
}
