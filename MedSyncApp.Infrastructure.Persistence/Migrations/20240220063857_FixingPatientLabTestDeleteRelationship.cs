using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedSyncApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixingPatientLabTestDeleteRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabTests_Patients_PatientId",
                table: "LabTests");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTests_Patients_PatientId",
                table: "LabTests",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabTests_Patients_PatientId",
                table: "LabTests");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTests_Patients_PatientId",
                table: "LabTests",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
