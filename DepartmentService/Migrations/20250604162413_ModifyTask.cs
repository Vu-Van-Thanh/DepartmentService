using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepartmentService.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Attachments",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440010"),
                column: "Attachments",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440011"),
                column: "Attachments",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440012"),
                column: "Attachments",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachments",
                table: "Tasks");
        }
    }
}
