using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepartmentService.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "AppliedCvs",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "AppliedCvs",
                newName: "status");
        }
    }
}
