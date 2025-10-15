using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Data.Migrations
{
    /// <inheritdoc />
    public partial class ArreglosDamnificadosSalida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Damnificados_Vehiculo_VehiculoDamnificadoId",
                table: "Damnificados");

            migrationBuilder.DropForeignKey(
                name: "FK_Damnificados_Vehiculo_VehiculoPasajeroId",
                table: "Damnificados");

            migrationBuilder.DropForeignKey(
                name: "FK_fuerzaInterviniente_Salidas_Fuerzas_FuerzaIntervinienteId",
                table: "fuerzaInterviniente_Salidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculo_Damnificados_ConductorDamnificadoId",
                table: "Vehiculo");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculo_Seguro_SeguroId",
                table: "Vehiculo");

            migrationBuilder.DropIndex(
                name: "IX_Vehiculo_ConductorDamnificadoId",
                table: "Vehiculo");

            migrationBuilder.DropIndex(
                name: "IX_Damnificados_VehiculoDamnificadoId",
                table: "Damnificados");

            migrationBuilder.DropColumn(
                name: "ConductorDamnificadoId",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "VehiculoDamnificadoId",
                table: "Damnificados");

            migrationBuilder.RenameColumn(
                name: "VehiculoPasajeroId",
                table: "Damnificados",
                newName: "FuerzaIntervinienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Damnificados_VehiculoPasajeroId",
                table: "Damnificados",
                newName: "IX_Damnificados_FuerzaIntervinienteId");

            migrationBuilder.AddColumn<string>(
                name: "NombreYApellidoDuenio",
                table: "Vehiculo",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "VehiculoId",
                table: "Seguro",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "Damnificados",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Damnificados",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Estado",
                table: "Damnificados",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Edad",
                table: "Damnificados",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Documento",
                table: "Damnificados",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Apellido",
                table: "Damnificados",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Destino",
                table: "Damnificados",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OcupantesVehiculos",
                columns: table => new
                {
                    OcupanteVehiculoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VehiculoAfectadoId = table.Column<int>(type: "int", nullable: false),
                    DamnificadoSalidaId = table.Column<int>(type: "int", nullable: false),
                    Rol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcupantesVehiculos", x => x.OcupanteVehiculoId);
                    table.ForeignKey(
                        name: "FK_OcupantesVehiculos_Damnificados_DamnificadoSalidaId",
                        column: x => x.DamnificadoSalidaId,
                        principalTable: "Damnificados",
                        principalColumn: "Damnificado_SalidaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OcupantesVehiculos_Vehiculo_VehiculoAfectadoId",
                        column: x => x.VehiculoAfectadoId,
                        principalTable: "Vehiculo",
                        principalColumn: "VehiculoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Seguro_VehiculoId",
                table: "Seguro",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_OcupantesVehiculos_DamnificadoSalidaId",
                table: "OcupantesVehiculos",
                column: "DamnificadoSalidaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OcupantesVehiculos_VehiculoAfectadoId",
                table: "OcupantesVehiculos",
                column: "VehiculoAfectadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Damnificados_fuerzaInterviniente_Salidas_FuerzaInterviniente~",
                table: "Damnificados",
                column: "FuerzaIntervinienteId",
                principalTable: "fuerzaInterviniente_Salidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_fuerzaInterviniente_Salidas_Fuerzas_FuerzaIntervinienteId",
                table: "fuerzaInterviniente_Salidas",
                column: "FuerzaIntervinienteId",
                principalTable: "Fuerzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seguro_Vehiculo_VehiculoId",
                table: "Seguro",
                column: "VehiculoId",
                principalTable: "Vehiculo",
                principalColumn: "VehiculoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculo_Seguro_SeguroId",
                table: "Vehiculo",
                column: "SeguroId",
                principalTable: "Seguro",
                principalColumn: "SeguroId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Damnificados_fuerzaInterviniente_Salidas_FuerzaInterviniente~",
                table: "Damnificados");

            migrationBuilder.DropForeignKey(
                name: "FK_fuerzaInterviniente_Salidas_Fuerzas_FuerzaIntervinienteId",
                table: "fuerzaInterviniente_Salidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Seguro_Vehiculo_VehiculoId",
                table: "Seguro");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculo_Seguro_SeguroId",
                table: "Vehiculo");

            migrationBuilder.DropTable(
                name: "OcupantesVehiculos");

            migrationBuilder.DropIndex(
                name: "IX_Seguro_VehiculoId",
                table: "Seguro");

            migrationBuilder.DropColumn(
                name: "NombreYApellidoDuenio",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "VehiculoId",
                table: "Seguro");

            migrationBuilder.DropColumn(
                name: "Destino",
                table: "Damnificados");

            migrationBuilder.RenameColumn(
                name: "FuerzaIntervinienteId",
                table: "Damnificados",
                newName: "VehiculoPasajeroId");

            migrationBuilder.RenameIndex(
                name: "IX_Damnificados_FuerzaIntervinienteId",
                table: "Damnificados",
                newName: "IX_Damnificados_VehiculoPasajeroId");

            migrationBuilder.AddColumn<int>(
                name: "ConductorDamnificadoId",
                table: "Vehiculo",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Damnificados",
                keyColumn: "Sexo",
                keyValue: null,
                column: "Sexo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "Damnificados",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Damnificados",
                keyColumn: "Nombre",
                keyValue: null,
                column: "Nombre",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Damnificados",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Estado",
                table: "Damnificados",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Edad",
                table: "Damnificados",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Documento",
                table: "Damnificados",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Damnificados",
                keyColumn: "Apellido",
                keyValue: null,
                column: "Apellido",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Apellido",
                table: "Damnificados",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "VehiculoDamnificadoId",
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
                name: "FK_fuerzaInterviniente_Salidas_Fuerzas_FuerzaIntervinienteId",
                table: "fuerzaInterviniente_Salidas",
                column: "FuerzaIntervinienteId",
                principalTable: "Fuerzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculo_Damnificados_ConductorDamnificadoId",
                table: "Vehiculo",
                column: "ConductorDamnificadoId",
                principalTable: "Damnificados",
                principalColumn: "Damnificado_SalidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculo_Seguro_SeguroId",
                table: "Vehiculo",
                column: "SeguroId",
                principalTable: "Seguro",
                principalColumn: "SeguroId");
        }
    }
}
