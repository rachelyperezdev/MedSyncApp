using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedSyncApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SettingLabResultToAcceptDuplicateLabTestId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LabResults_LabTestId",
                table: "LabResults");

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_LabTestId",
                table: "LabResults",
                column: "LabTestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LabResults_LabTestId",
                table: "LabResults");

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_LabTestId",
                table: "LabResults",
                column: "LabTestId",
                unique: true);
        }
    }
}
