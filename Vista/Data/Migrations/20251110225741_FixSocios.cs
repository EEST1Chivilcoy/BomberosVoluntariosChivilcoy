using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixSocios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistorialesSocios_Persona_CobradorQueCobroPersonaId",
                table: "HistorialesSocios");

            migrationBuilder.DropIndex(
                name: "IX_HistorialesSocios_CobradorQueCobroPersonaId",
                table: "HistorialesSocios");

            migrationBuilder.DropColumn(
                name: "CobradorQueCobroPersonaId",
                table: "HistorialesSocios");

            migrationBuilder.DropColumn(
                name: "FormaDePago",
                table: "HistorialesSocios");

            migrationBuilder.DropColumn(
                name: "Monto",
                table: "HistorialesSocios");

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "Socios",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Ocupacion",
                table: "Socios",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentoOCUIT",
                table: "Socios",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Socios",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Socios");

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "Socios",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Ocupacion",
                table: "Socios",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentoOCUIT",
                table: "Socios",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CobradorQueCobroPersonaId",
                table: "HistorialesSocios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FormaDePago",
                table: "HistorialesSocios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Monto",
                table: "HistorialesSocios",
                type: "double",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistorialesSocios_CobradorQueCobroPersonaId",
                table: "HistorialesSocios",
                column: "CobradorQueCobroPersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistorialesSocios_Persona_CobradorQueCobroPersonaId",
                table: "HistorialesSocios",
                column: "CobradorQueCobroPersonaId",
                principalTable: "Persona",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
