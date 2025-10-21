using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixMovimientoEquipoAutonomo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimientosEquiposAutonomos_Dependencias_DependenciaAgenteDe~",
                table: "MovimientosEquiposAutonomos");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimientosEquiposAutonomos_Vehiculo_VehiculoAgenteVehiculoId",
                table: "MovimientosEquiposAutonomos");

            migrationBuilder.RenameColumn(
                name: "VehiculoAgenteVehiculoId",
                table: "MovimientosEquiposAutonomos",
                newName: "VehiculoDestinoId");

            migrationBuilder.RenameColumn(
                name: "DependenciaAgenteDependenciaId",
                table: "MovimientosEquiposAutonomos",
                newName: "DependenciaDestinoId");

            migrationBuilder.RenameIndex(
                name: "IX_MovimientosEquiposAutonomos_VehiculoAgenteVehiculoId",
                table: "MovimientosEquiposAutonomos",
                newName: "IX_MovimientosEquiposAutonomos_VehiculoDestinoId");

            migrationBuilder.RenameIndex(
                name: "IX_MovimientosEquiposAutonomos_DependenciaAgenteDependenciaId",
                table: "MovimientosEquiposAutonomos",
                newName: "IX_MovimientosEquiposAutonomos_DependenciaDestinoId");

            migrationBuilder.AddColumn<string>(
                name: "AgenteAnterior",
                table: "MovimientosEquiposAutonomos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OtroDestino",
                table: "MovimientosEquiposAutonomos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimientosEquiposAutonomos_Dependencias_DependenciaDestinoId",
                table: "MovimientosEquiposAutonomos",
                column: "DependenciaDestinoId",
                principalTable: "Dependencias",
                principalColumn: "DependenciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimientosEquiposAutonomos_Vehiculo_VehiculoDestinoId",
                table: "MovimientosEquiposAutonomos",
                column: "VehiculoDestinoId",
                principalTable: "Vehiculo",
                principalColumn: "VehiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimientosEquiposAutonomos_Dependencias_DependenciaDestinoId",
                table: "MovimientosEquiposAutonomos");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimientosEquiposAutonomos_Vehiculo_VehiculoDestinoId",
                table: "MovimientosEquiposAutonomos");

            migrationBuilder.DropColumn(
                name: "AgenteAnterior",
                table: "MovimientosEquiposAutonomos");

            migrationBuilder.DropColumn(
                name: "OtroDestino",
                table: "MovimientosEquiposAutonomos");

            migrationBuilder.RenameColumn(
                name: "VehiculoDestinoId",
                table: "MovimientosEquiposAutonomos",
                newName: "VehiculoAgenteVehiculoId");

            migrationBuilder.RenameColumn(
                name: "DependenciaDestinoId",
                table: "MovimientosEquiposAutonomos",
                newName: "DependenciaAgenteDependenciaId");

            migrationBuilder.RenameIndex(
                name: "IX_MovimientosEquiposAutonomos_VehiculoDestinoId",
                table: "MovimientosEquiposAutonomos",
                newName: "IX_MovimientosEquiposAutonomos_VehiculoAgenteVehiculoId");

            migrationBuilder.RenameIndex(
                name: "IX_MovimientosEquiposAutonomos_DependenciaDestinoId",
                table: "MovimientosEquiposAutonomos",
                newName: "IX_MovimientosEquiposAutonomos_DependenciaAgenteDependenciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimientosEquiposAutonomos_Dependencias_DependenciaAgenteDe~",
                table: "MovimientosEquiposAutonomos",
                column: "DependenciaAgenteDependenciaId",
                principalTable: "Dependencias",
                principalColumn: "DependenciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimientosEquiposAutonomos_Vehiculo_VehiculoAgenteVehiculoId",
                table: "MovimientosEquiposAutonomos",
                column: "VehiculoAgenteVehiculoId",
                principalTable: "Vehiculo",
                principalColumn: "VehiculoId");
        }
    }
}
