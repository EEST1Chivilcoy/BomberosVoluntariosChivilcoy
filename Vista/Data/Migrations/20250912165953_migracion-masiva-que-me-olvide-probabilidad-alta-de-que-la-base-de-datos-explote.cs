using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class migracionmasivaquemeolvideprobabilidadaltadequelabasededatosexplote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculo_Damnificados_DamnificadoId",
                table: "Vehiculo");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculo_Salidas_SalidaId",
                table: "Vehiculo");

            migrationBuilder.DropIndex(
                name: "IX_Vehiculo_DamnificadoId",
                table: "Vehiculo");

            migrationBuilder.DropIndex(
                name: "IX_Vehiculo_SalidaId",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "DamnificadoId",
                table: "Vehiculo");

            migrationBuilder.RenameColumn(
                name: "VehiculoAfectado_Color",
                table: "Vehiculo",
                newName: "VehiculoAfectado_Observaciones");

            migrationBuilder.RenameColumn(
                name: "VehiculoAfectado_Airbag",
                table: "Vehiculo",
                newName: "seConoceConductor");

            migrationBuilder.RenameColumn(
                name: "SalidaId",
                table: "Vehiculo",
                newName: "ConductorDamnificadoId");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Vehiculo",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "VehiculoDamnificadoId",
                table: "Damnificados",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehiculoPasajeroId",
                table: "Damnificados",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_ConductorDamnificadoId",
                table: "Vehiculo",
                column: "ConductorDamnificadoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Damnificados_VehiculoDamnificadoId",
                table: "Damnificados",
                column: "VehiculoDamnificadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Damnificados_VehiculoPasajeroId",
                table: "Damnificados",
                column: "VehiculoPasajeroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Damnificados_Vehiculo_VehiculoDamnificadoId",
                table: "Damnificados",
                column: "VehiculoDamnificadoId",
                principalTable: "Vehiculo",
                principalColumn: "VehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Damnificados_Vehiculo_VehiculoPasajeroId",
                table: "Damnificados",
                column: "VehiculoPasajeroId",
                principalTable: "Vehiculo",
                principalColumn: "VehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculo_Damnificados_ConductorDamnificadoId",
                table: "Vehiculo",
                column: "ConductorDamnificadoId",
                principalTable: "Damnificados",
                principalColumn: "Damnificado_SalidaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Damnificados_Vehiculo_VehiculoDamnificadoId",
                table: "Damnificados");

            migrationBuilder.DropForeignKey(
                name: "FK_Damnificados_Vehiculo_VehiculoPasajeroId",
                table: "Damnificados");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculo_Damnificados_ConductorDamnificadoId",
                table: "Vehiculo");

            migrationBuilder.DropIndex(
                name: "IX_Vehiculo_ConductorDamnificadoId",
                table: "Vehiculo");

            migrationBuilder.DropIndex(
                name: "IX_Damnificados_VehiculoDamnificadoId",
                table: "Damnificados");

            migrationBuilder.DropIndex(
                name: "IX_Damnificados_VehiculoPasajeroId",
                table: "Damnificados");

            migrationBuilder.DropColumn(
                name: "VehiculoDamnificadoId",
                table: "Damnificados");

            migrationBuilder.DropColumn(
                name: "VehiculoPasajeroId",
                table: "Damnificados");

            migrationBuilder.RenameColumn(
                name: "seConoceConductor",
                table: "Vehiculo",
                newName: "VehiculoAfectado_Airbag");

            migrationBuilder.RenameColumn(
                name: "VehiculoAfectado_Observaciones",
                table: "Vehiculo",
                newName: "VehiculoAfectado_Color");

            migrationBuilder.RenameColumn(
                name: "ConductorDamnificadoId",
                table: "Vehiculo",
                newName: "SalidaId");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Vehiculo",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "DamnificadoId",
                table: "Vehiculo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_DamnificadoId",
                table: "Vehiculo",
                column: "DamnificadoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_SalidaId",
                table: "Vehiculo",
                column: "SalidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculo_Damnificados_DamnificadoId",
                table: "Vehiculo",
                column: "DamnificadoId",
                principalTable: "Damnificados",
                principalColumn: "Damnificado_SalidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculo_Salidas_SalidaId",
                table: "Vehiculo",
                column: "SalidaId",
                principalTable: "Salidas",
                principalColumn: "SalidaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
