using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFechaVencimientoYFechaUltimaPruebaToEquipoAutonomo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaVencimientoPruebaHidraulica",
                table: "EquiposAutonomos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaFechaPruebaHidraulica",
                table: "EquiposAutonomos",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaVencimientoPruebaHidraulica",
                table: "EquiposAutonomos");

            migrationBuilder.DropColumn(
                name: "UltimaFechaPruebaHidraulica",
                table: "EquiposAutonomos");
        }
    }
}
