using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DapperRabbitMQ.Migrations
{
    /// <inheritdoc />
    public partial class Department : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "CreatedOn", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { "1", new DateTime(2026, 11, 18, 11, 57, 34, 399, DateTimeKind.Utc).AddTicks(5200), "Human Resources", true, "HR" },
                    { "2", new DateTime(2026, 11, 18, 11, 57, 34, 399, DateTimeKind.Utc).AddTicks(5480), "Information Technology", true, "IT" },
                    { "3", new DateTime(2026, 11, 18, 11, 57, 34, 399, DateTimeKind.Utc).AddTicks(5480), "Finance and Accounting", true, "Finance" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
