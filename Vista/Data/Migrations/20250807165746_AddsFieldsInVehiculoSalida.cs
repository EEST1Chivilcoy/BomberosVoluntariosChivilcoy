using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddsFieldsInVehiculoSalida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CantidadAceite",
                table: "Vehiculo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Combustible",
                table: "Vehiculo",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaProximoService",
                table: "Vehiculo",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaUltimoService",
                table: "Vehiculo",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarcaAceite",
                table: "Vehiculo",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TipoAceite",
                table: "Vehiculo",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "VehiculoAfectado_Color",
                table: "Vehiculo",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadAceite",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "Combustible",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "FechaProximoService",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "FechaUltimoService",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "MarcaAceite",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "TipoAceite",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "VehiculoAfectado_Color",
                table: "Vehiculo");
        }
    }
}
