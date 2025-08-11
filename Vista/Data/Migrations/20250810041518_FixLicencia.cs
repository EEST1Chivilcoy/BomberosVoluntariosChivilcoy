using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixLicencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licencias_Persona_PersonalId",
                table: "Licencias");

            migrationBuilder.AddForeignKey(
                name: "FK_Licencias_Persona_PersonalId",
                table: "Licencias",
                column: "PersonalId",
                principalTable: "Persona",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licencias_Persona_PersonalId",
                table: "Licencias");

            migrationBuilder.AddForeignKey(
                name: "FK_Licencias_Persona_PersonalId",
                table: "Licencias",
                column: "PersonalId",
                principalTable: "Persona",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
