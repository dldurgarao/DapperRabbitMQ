using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DapperRabbitMQ.Migrations
{
    /// <inheritdoc />
    public partial class Depar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: "3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "CreatedOn", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { "1", new DateTime(2026, 11, 18, 12, 4, 21, 525, DateTimeKind.Utc).AddTicks(440), "Human Resources", true, "HR" },
                    { "2", new DateTime(2026, 11, 18, 12, 4, 21, 525, DateTimeKind.Utc).AddTicks(610), "Information Technology", true, "IT" },
                    { "3", new DateTime(2026, 11, 18, 12, 4, 21, 525, DateTimeKind.Utc).AddTicks(610), "Finance and Accounting", true, "Finance" }
                });
        }
    }
}
