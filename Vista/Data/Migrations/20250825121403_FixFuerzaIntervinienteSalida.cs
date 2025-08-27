using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixFuerzaIntervinienteSalida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fuerzaIntervinientes");

            migrationBuilder.CreateTable(
                name: "fuerzaInterviniente_Salidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NumeroUnidad = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EncargadoApellidoyNombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FuerzaIntervinienteId = table.Column<int>(type: "int", nullable: false),
                    SalidaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fuerzaInterviniente_Salidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fuerzaInterviniente_Salidas_Fuerzas_FuerzaIntervinienteId",
                        column: x => x.FuerzaIntervinienteId,
                        principalTable: "Fuerzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fuerzaInterviniente_Salidas_Salidas_SalidaId",
                        column: x => x.SalidaId,
                        principalTable: "Salidas",
                        principalColumn: "SalidaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_fuerzaInterviniente_Salidas_FuerzaIntervinienteId",
                table: "fuerzaInterviniente_Salidas",
                column: "FuerzaIntervinienteId");

            migrationBuilder.CreateIndex(
                name: "IX_fuerzaInterviniente_Salidas_SalidaId",
                table: "fuerzaInterviniente_Salidas",
                column: "SalidaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fuerzaInterviniente_Salidas");

            migrationBuilder.CreateTable(
                name: "fuerzaIntervinientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Damnificado_SalidaId = table.Column<int>(type: "int", nullable: true),
                    FuerzaId = table.Column<int>(type: "int", nullable: false),
                    SalidaId = table.Column<int>(type: "int", nullable: false),
                    VehiculoId = table.Column<int>(type: "int", nullable: false),
                    Destino = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Encargado = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fuerzaIntervinientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fuerzaIntervinientes_Damnificados_Damnificado_SalidaId",
                        column: x => x.Damnificado_SalidaId,
                        principalTable: "Damnificados",
                        principalColumn: "Damnificado_SalidaId");
                    table.ForeignKey(
                        name: "FK_fuerzaIntervinientes_Fuerzas_FuerzaId",
                        column: x => x.FuerzaId,
                        principalTable: "Fuerzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fuerzaIntervinientes_Salidas_SalidaId",
                        column: x => x.SalidaId,
                        principalTable: "Salidas",
                        principalColumn: "SalidaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_fuerzaIntervinientes_Vehiculo_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculo",
                        principalColumn: "VehiculoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_fuerzaIntervinientes_Damnificado_SalidaId",
                table: "fuerzaIntervinientes",
                column: "Damnificado_SalidaId");

            migrationBuilder.CreateIndex(
                name: "IX_fuerzaIntervinientes_FuerzaId",
                table: "fuerzaIntervinientes",
                column: "FuerzaId");

            migrationBuilder.CreateIndex(
                name: "IX_fuerzaIntervinientes_SalidaId",
                table: "fuerzaIntervinientes",
                column: "SalidaId");

            migrationBuilder.CreateIndex(
                name: "IX_fuerzaIntervinientes_VehiculoId",
                table: "fuerzaIntervinientes",
                column: "VehiculoId");
        }
    }
}
