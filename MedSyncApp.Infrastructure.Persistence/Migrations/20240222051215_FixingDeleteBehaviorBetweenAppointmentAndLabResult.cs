using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedSyncApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixingDeleteBehaviorBetweenAppointmentAndLabResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabResults_Appointments_AppointmentId",
                table: "LabResults");

            migrationBuilder.AddForeignKey(
                name: "FK_LabResults_Appointments_AppointmentId",
                table: "LabResults",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabResults_Appointments_AppointmentId",
                table: "LabResults");

            migrationBuilder.AddForeignKey(
                name: "FK_LabResults_Appointments_AppointmentId",
                table: "LabResults",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");
        }
    }
}
