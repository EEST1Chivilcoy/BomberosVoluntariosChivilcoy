using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixesInPagosSocio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PagoSocio_Persona_CobradorId",
                table: "PagoSocio");

            migrationBuilder.DropColumn(
                name: "FechaCobro",
                table: "PagoSocio");

            migrationBuilder.RenameColumn(
                name: "FechaGeneradoPendiente",
                table: "PagoSocio",
                newName: "Fecha");

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "PagoSocio",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_PagoSocio_Persona_CobradorId",
                table: "PagoSocio",
                column: "CobradorId",
                principalTable: "Persona",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PagoSocio_Persona_CobradorId",
                table: "PagoSocio");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "PagoSocio");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "PagoSocio",
                newName: "FechaGeneradoPendiente");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCobro",
                table: "PagoSocio",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_PagoSocio_Persona_CobradorId",
                table: "PagoSocio",
                column: "CobradorId",
                principalTable: "Persona",
                principalColumn: "PersonaId");
        }
    }
}
