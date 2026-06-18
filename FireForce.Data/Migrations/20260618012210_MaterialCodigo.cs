using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireForce.Core.Data.Migrations
{
    /// <inheritdoc />
    public partial class MaterialCodigo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE `Materiales`
                SET `Codigo` = CONCAT(
                    SUBSTRING(LPAD(`MaterialId`, 12, '0'), 1, 3), '-',
                    SUBSTRING(LPAD(`MaterialId`, 12, '0'), 4, 3), '-',
                    SUBSTRING(LPAD(`MaterialId`, 12, '0'), 7, 3), '-',
                    SUBSTRING(LPAD(`MaterialId`, 12, '0'), 10, 3)
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE `Materiales` SET `Codigo` = NULL;");
        }
    }
}
