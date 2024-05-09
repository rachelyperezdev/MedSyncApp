using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedSyncApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingAppointmentLabTestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabTests_Appointments_AppointmentId",
                table: "LabTests");

            migrationBuilder.DropForeignKey(
                name: "FK_LabTests_Patients_PatientId",
                table: "LabTests");

            migrationBuilder.DropIndex(
                name: "IX_LabTests_AppointmentId",
                table: "LabTests");

            migrationBuilder.DropIndex(
                name: "IX_LabTests_PatientId",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "LabTests");

            migrationBuilder.CreateTable(
                name: "AppointmentLabTest",
                columns: table => new
                {
                    AppointmentsId = table.Column<int>(type: "int", nullable: false),
                    LabTestsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentLabTest", x => new { x.AppointmentsId, x.LabTestsId });
                    table.ForeignKey(
                        name: "FK_AppointmentLabTest_Appointments_AppointmentsId",
                        column: x => x.AppointmentsId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentLabTest_LabTests_LabTestsId",
                        column: x => x.LabTestsId,
                        principalTable: "LabTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentLabTest_LabTestsId",
                table: "AppointmentLabTest",
                column: "LabTestsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentLabTest");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "LabTests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "LabTests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_AppointmentId",
                table: "LabTests",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_PatientId",
                table: "LabTests",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTests_Appointments_AppointmentId",
                table: "LabTests",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTests_Patients_PatientId",
                table: "LabTests",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
