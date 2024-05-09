using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedSyncApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixingLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "LabResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "LabResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_AppointmentId",
                table: "LabResults",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_PatientId",
                table: "LabResults",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabResults_Appointments_AppointmentId",
                table: "LabResults",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LabResults_Patients_PatientId",
                table: "LabResults",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabResults_Appointments_AppointmentId",
                table: "LabResults");

            migrationBuilder.DropForeignKey(
                name: "FK_LabResults_Patients_PatientId",
                table: "LabResults");

            migrationBuilder.DropIndex(
                name: "IX_LabResults_AppointmentId",
                table: "LabResults");

            migrationBuilder.DropIndex(
                name: "IX_LabResults_PatientId",
                table: "LabResults");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "LabResults");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "LabResults");
        }
    }
}
