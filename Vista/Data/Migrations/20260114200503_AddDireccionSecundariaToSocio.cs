using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDireccionSecundariaToSocio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DireccionSecundaria",
                table: "Socios",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "LatitudSecundaria",
                table: "Socios",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LongitudSecundaria",
                table: "Socios",
                type: "double",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DireccionSecundaria",
                table: "Socios");

            migrationBuilder.DropColumn(
                name: "LatitudSecundaria",
                table: "Socios");

            migrationBuilder.DropColumn(
                name: "LongitudSecundaria",
                table: "Socios");
        }
    }
}
