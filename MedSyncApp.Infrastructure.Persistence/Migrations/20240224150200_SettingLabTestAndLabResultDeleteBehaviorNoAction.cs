using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedSyncApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SettingLabTestAndLabResultDeleteBehaviorNoAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabResults_LabTests_LabTestId",
                table: "LabResults");

            migrationBuilder.AddForeignKey(
                name: "FK_LabResults_LabTests_LabTestId",
                table: "LabResults",
                column: "LabTestId",
                principalTable: "LabTests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabResults_LabTests_LabTestId",
                table: "LabResults");

            migrationBuilder.AddForeignKey(
                name: "FK_LabResults_LabTests_LabTestId",
                table: "LabResults",
                column: "LabTestId",
                principalTable: "LabTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
