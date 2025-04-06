using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DepartmentService.Migrations
{
    /// <inheritdoc />
    public partial class Inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Manager = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Articles_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    FacId = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    DepartmentId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UsedQuantity = table.Column<int>(type: "int", nullable: false),
                    Condition = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastMaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.FacId);
                    table.ForeignKey(
                        name: "FK_Facilities_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobPositions",
                columns: table => new
                {
                    JobPositionId = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    PositionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Manager = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPositions", x => x.JobPositionId);
                    table.ForeignKey(
                        name: "FK_JobPositions_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    MediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArticleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaContent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.MediaID);
                    table.ForeignKey(
                        name: "FK_Medias_Articles_ArticleID",
                        column: x => x.ArticleID,
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OwnFacilities",
                columns: table => new
                {
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityId = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnFacilities", x => new { x.OwnerId, x.FacilityId });
                    table.ForeignKey(
                        name: "FK_OwnFacilities_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "FacId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedTo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Tasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "Contact", "DepartmentName", "Description", "Location", "Manager" },
                values: new object[,]
                {
                    { "FN", "+1-555-9876", "Finance", "Manages company finances and budget planning.", "Chicago", "Robert Brown" },
                    { "HR", "+1-555-1234", "Human Resources", "Responsible for employee relations and HR policies.", "New York", "John Doe" },
                    { "IT", "+1-555-5678", "Information Technology", "Handles all IT infrastructure and software development.", "San Francisco", "Alice Smith" }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "ArticleId", "Content", "DateCreated", "DepartmentId", "Title", "Type" },
                values: new object[,]
                {
                    { new Guid("18fc6183-8a57-4deb-9d26-f28034395fc1"), "Phòng Nhân sự công bố báo cáo tổng kết hoạt động tuyển dụng trong năm 2023, bao gồm số lượng ứng viên, tỷ lệ trúng tuyển và hiệu quả các kênh tuyển dụng.", new DateTime(2024, 9, 30, 11, 30, 0, 0, DateTimeKind.Unspecified), "FN", "Báo cáo tuyển dụng năm 2023", "Normal" },
                    { new Guid("24e5b629-cd94-4364-bb64-63e5a7e72dda"), "Công ty tổ chức đánh giá định kỳ các chương trình đào tạo nội bộ để cải thiện nội dung và phương pháp giảng dạy, giúp nâng cao hiệu quả học tập của nhân viên.", new DateTime(2024, 10, 4, 15, 45, 0, 0, DateTimeKind.Unspecified), "IT", "Đánh giá chất lượng đào tạo nội bộ", "Normal" },
                    { new Guid("32460e72-2ba1-420f-b32c-53f4e2e01be3"), "Ứng viên cần chuẩn bị hồ sơ đầy đủ theo hướng dẫn của Phòng Nhân sự, bao gồm CV, bằng cấp, chứng chỉ và các giấy tờ liên quan trước khi tham gia phỏng vấn.", new DateTime(2024, 10, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), "FN", "Hướng dẫn chuẩn bị hồ sơ ứng tuyển", "Normal" },
                    { new Guid("53aa8629-51a7-4810-8e69-0cc8a4c4ebd3"), "Phòng Nhân sự thông báo lịch phỏng vấn tuyển dụng cho các vị trí trong đợt tuyển dụng tháng 10/2024. Ứng viên cần theo dõi lịch cụ thể để tham gia đúng giờ.", new DateTime(2024, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "IT", "Thông báo lịch phỏng vấn tuyển dụng", "Normal" },
                    { new Guid("9aa5e0da-57d8-4393-b9c9-5a30374f0703"), "Công ty triển khai nhiều chương trình đào tạo nội bộ nhằm nâng cao kỹ năng chuyên môn và năng lực quản lý cho nhân viên ở các cấp.", new DateTime(2024, 10, 2, 12, 15, 0, 0, DateTimeKind.Unspecified), "IT", "Giới thiệu chương trình đào tạo nội bộ", "Normal" },
                    { new Guid("a14fb4b4-9886-437b-add1-e4429424f6a2"), "Để đáp ứng nhu cầu mở rộng và phát triển nhân sự, Công ty đưa ra kế hoạch tuyển dụng chi tiết cho năm 2024-2025, bao gồm các vị trí cần thiết và tiêu chí lựa chọn ứng viên.", new DateTime(2024, 10, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), "HR", "Kế hoạch tuyển dụng năm 2024-2025", "Normal" }
                });

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "FacId", "Condition", "DepartmentId", "Description", "LastMaintenanceDate", "Name", "PurchaseDate", "Quantity", "UsedQuantity" },
                values: new object[,]
                {
                    { "FAC001", 0, "HR", "Conference room projector", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Projector", new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { "FAC002", 1, "HR", "Ergonomic office chairs for HR department", new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Office Chairs", new DateTime(2022, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, 18 },
                    { "FAC003", 2, "IT", "Data center server rack", new DateTime(2023, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Server Rack", new DateTime(2021, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5 },
                    { "FAC004", 1, "IT", "High-performance computers for software development", new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Developer Workstations", new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 9 },
                    { "FAC005", 2, "FN", "Enterprise license for financial software", null, "Accounting Software License", new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "JobPositions",
                columns: new[] { "JobPositionId", "DepartmentId", "Description", "Level", "Manager", "PositionName" },
                values: new object[,]
                {
                    { "FNA01", "FN", "Analyzes financial data and prepares reports.", 3, "Robert Brown", "Finance Analyst" },
                    { "FNA02", "FN", "Manages company financial records and transactions.", 2, "Emily Davis", "Accountant" },
                    { "HRM01", "HR", "Responsible for managing HR team and policies.", 1, "John Doe", "HR Manager" },
                    { "HRR02", "HR", "Handles recruitment and interviewing processes.", 2, "Jane Smith", "Recruitment Specialist" },
                    { "ITS01", "IT", "Develops and maintains software applications.", 0, "Alice Johnson", "Software Engineer" },
                    { "ITS02", "IT", "Manages IT infrastructure and networks.", 1, "Bob Williams", "System Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "CreatedAt", "CreatedBy", "DepartmentId", "Description", "EndDate", "Name", "StartDate", "Status", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("550e8400-e29b-41d4-a716-446655440000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "FN", "Phát triển hệ thống quản lý nhân sự", new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "HRM System Development", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "In Progress", null, null },
                    { new Guid("550e8400-e29b-41d4-a716-446655440002"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "FN", "Xây dựng nền tảng học trực tuyến", new DateTime(2024, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "E-Learning Platform", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Planning", null, null },
                    { new Guid("550e8400-e29b-41d4-a716-446655440003"), new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "IT", "Phát triển ứng dụng di động", new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mobile App Development", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Planning", null, null }
                });

            migrationBuilder.InsertData(
                table: "Medias",
                columns: new[] { "MediaID", "ArticleID", "MediaContent", "MediaType", "MediaUrl" },
                values: new object[,]
                {
                    { new Guid("266cc1fd-8d23-461a-bbed-17d8d77ad97b"), new Guid("18fc6183-8a57-4deb-9d26-f28034395fc1"), null, "video/mp4", "https://www.youtube.com/watch?v=8yVew7OrJdI" },
                    { new Guid("3f64434c-0c5e-4b28-9e87-ef0562c9536c"), new Guid("a14fb4b4-9886-437b-add1-e4429424f6a2"), null, "image", "https://firebasestorage.googleapis.com/v0/b/tsdhbk-632bb.appspot.com/o/slide2_1.png?alt=media&token=bc2ae7c2-f47e-4883-a600-7cc5c34e9c5e" },
                    { new Guid("9f2b04f0-4a38-4abc-823d-b83e618ff4ad"), new Guid("32460e72-2ba1-420f-b32c-53f4e2e01be3"), null, "image", "https://firebasestorage.googleapis.com/v0/b/tsdhbk-632bb.appspot.com/o/HY%2Ftd6.jpg?alt=media&token=a3878a22-1a97-40ae-bbbe-2cd3eac03cf9" },
                    { new Guid("bd669f86-a9c3-476e-8975-1efccf6ad3c5"), new Guid("9aa5e0da-57d8-4393-b9c9-5a30374f0703"), null, "image", "https://firebasestorage.googleapis.com/v0/b/tsdhbk-632bb.appspot.com/o/xttn%2FPOST-NG-a-NG-Ia-M-XTTN-2024-XT-6188-2429-1718331174.png?alt=media&token=6aba61bc-23b6-4f31-8d8d-5f81c1586951" },
                    { new Guid("c87767bd-dbfa-4416-a500-ee8bf14227f2"), new Guid("53aa8629-51a7-4810-8e69-0cc8a4c4ebd3"), null, "video/mp4", "https://www.youtube.com/watch?v=xLrHnIiAack" }
                });

            migrationBuilder.InsertData(
                table: "OwnFacilities",
                columns: new[] { "FacilityId", "OwnerId" },
                values: new object[,]
                {
                    { "FAC001", new Guid("63e29a28-bc90-4cd1-8f9e-ab9834bdde6c") },
                    { "FAC004", new Guid("63e29a28-bc90-4cd1-8f9e-ab9834bdde6c") },
                    { "FAC005", new Guid("63e29a28-bc90-4cd1-8f9e-ab9834bdde6c") },
                    { "FAC002", new Guid("d05780d4-5742-40ca-8403-0febd44b1555") }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskId", "AssignedTo", "CompletedAt", "CreatedAt", "CreatedBy", "Deadline", "Description", "Priority", "ProjectId", "StartDate", "Status", "Title", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("550e8400-e29b-41d4-a716-446655440010"), new Guid("550e8400-e29b-41d4-a716-446655440001"), new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thiết kế cấu trúc database cho hệ thống HRM", "High", new Guid("550e8400-e29b-41d4-a716-446655440000"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", "Thiết kế Database", new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440011"), new Guid("550e8400-e29b-41d4-a716-446655440002"), null, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xây dựng hệ thống xác thực và phân quyền", "High", new Guid("550e8400-e29b-41d4-a716-446655440000"), new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "In Progress", "Phát triển API Authentication", null, null },
                    { new Guid("550e8400-e29b-41d4-a716-446655440012"), new Guid("550e8400-e29b-41d4-a716-446655440003"), null, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thiết kế giao diện người dùng cho ứng dụng di động", "Medium", new Guid("550e8400-e29b-41d4-a716-446655440003"), new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "To Do", "Thiết kế UI/UX", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_DepartmentId",
                table: "Articles",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_DepartmentId",
                table: "Facilities",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPositions_DepartmentId",
                table: "JobPositions",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_ArticleID",
                table: "Medias",
                column: "ArticleID");

            migrationBuilder.CreateIndex(
                name: "IX_OwnFacilities_FacilityId",
                table: "OwnFacilities",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DepartmentId",
                table: "Projects",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobPositions");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "OwnFacilities");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
