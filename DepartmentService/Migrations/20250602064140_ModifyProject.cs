using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepartmentService.Migrations
{
    /// <inheritdoc />
    public partial class ModifyProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Members",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectAttachment",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectManager",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Visibility",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440000"),
                columns: new[] { "Members", "ProjectAttachment", "ProjectManager", "Visibility" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440002"),
                columns: new[] { "Members", "ProjectAttachment", "ProjectManager", "Visibility" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440003"),
                columns: new[] { "Members", "ProjectAttachment", "ProjectManager", "Visibility" },
                values: new object[] { null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Members",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectAttachment",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectManager",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "Projects");
        }
    }
}
