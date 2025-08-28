using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixVehiculosSalida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CajaVelocidades",
                table: "Vehiculo",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaUltCambioBateria",
                table: "Vehiculo",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LibrasCubiertas",
                table: "Vehiculo",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MarcaBateria",
                table: "Vehiculo",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MedidasCubiertas",
                table: "Vehiculo",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ModeloFiltroAire",
                table: "Vehiculo",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "Vehiculo",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TensionCElectrico",
                table: "Vehiculo",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TipoDireccion",
                table: "Vehiculo",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CajaVelocidades",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "FechaUltCambioBateria",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "LibrasCubiertas",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "MarcaBateria",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "MedidasCubiertas",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "ModeloFiltroAire",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "TensionCElectrico",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "TipoDireccion",
                table: "Vehiculo");
        }
    }
}
