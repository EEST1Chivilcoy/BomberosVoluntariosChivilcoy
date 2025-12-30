using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixHistorialSocios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MontoAnterior",
                table: "HistorialSocios",
                newName: "Monto");

            migrationBuilder.RenameColumn(
                name: "FrecuenciaDePagoAnterior",
                table: "HistorialSocios",
                newName: "FrecuenciaDePago");

            migrationBuilder.RenameColumn(
                name: "FormaDePagoAnterior",
                table: "HistorialSocios",
                newName: "FormaDePago");

            migrationBuilder.RenameColumn(
                name: "FechaDeCambio",
                table: "HistorialSocios",
                newName: "FechaDesde");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "HistorialSocios",
                newName: "FechaHasta");

            migrationBuilder.RenameColumn(
                name: "EstadoAnterior",
                table: "HistorialSocios",
                newName: "Estado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Monto",
                table: "HistorialSocios",
                newName: "MontoAnterior");

            migrationBuilder.RenameColumn(
                name: "FrecuenciaDePago",
                table: "HistorialSocios",
                newName: "FrecuenciaDePagoAnterior");

            migrationBuilder.RenameColumn(
                name: "FormaDePago",
                table: "HistorialSocios",
                newName: "FormaDePagoAnterior");

            migrationBuilder.RenameColumn(
                name: "FechaHasta",
                table: "HistorialSocios",
                newName: "Fecha");

            migrationBuilder.RenameColumn(
                name: "FechaDesde",
                table: "HistorialSocios",
                newName: "FechaDeCambio");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "HistorialSocios",
                newName: "EstadoAnterior");
        }
    }
}
