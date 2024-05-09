using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedSyncApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovingAppointmentLabTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentLabTest");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
