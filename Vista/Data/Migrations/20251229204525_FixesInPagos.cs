using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixesInPagos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PagoSocio_Persona_CobradorId",
                table: "PagoSocio");

            migrationBuilder.AddForeignKey(
                name: "FK_PagoSocio_Persona_CobradorId",
                table: "PagoSocio",
                column: "CobradorId",
                principalTable: "Persona",
                principalColumn: "PersonaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PagoSocio_Persona_CobradorId",
                table: "PagoSocio");

            migrationBuilder.AddForeignKey(
                name: "FK_PagoSocio_Persona_CobradorId",
                table: "PagoSocio",
                column: "CobradorId",
                principalTable: "Persona",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
