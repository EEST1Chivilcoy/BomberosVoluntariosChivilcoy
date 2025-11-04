using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class IncendioFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "seConoceConductor",
                table: "Vehiculo",
                newName: "SeConoceConductor");

            migrationBuilder.RenameColumn(
                name: "DeteccionAutomaticaId",
                table: "Salidas",
                newName: "DeteccionAutomatica");

            migrationBuilder.AlterColumn<double>(
                name: "Longitud",
                table: "Salidas",
                type: "double",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "Latitud",
                table: "Salidas",
                type: "double",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeConoceConductor",
                table: "Vehiculo",
                newName: "seConoceConductor");

            migrationBuilder.RenameColumn(
                name: "DeteccionAutomatica",
                table: "Salidas",
                newName: "DeteccionAutomaticaId");

            migrationBuilder.AlterColumn<double>(
                name: "Longitud",
                table: "Salidas",
                type: "double",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Latitud",
                table: "Salidas",
                type: "double",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);
        }
    }
}
