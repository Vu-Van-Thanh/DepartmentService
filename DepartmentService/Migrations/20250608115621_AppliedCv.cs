using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepartmentService.Migrations
{
    /// <inheritdoc />
    public partial class AppliedCv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppliedCvs",
                columns: table => new
                {
                    CVID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedCvs", x => x.CVID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppliedCvs");
        }
    }
}
