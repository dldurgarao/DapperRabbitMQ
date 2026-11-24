using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DapperRabbitMQ.Migrations
{
    /// <inheritdoc />
    public partial class Dep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: "1",
                column: "CreatedOn",
                value: new DateTime(2026, 11, 18, 12, 1, 58, 989, DateTimeKind.Utc).AddTicks(8050));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: "2",
                column: "CreatedOn",
                value: new DateTime(2026, 11, 18, 12, 1, 58, 989, DateTimeKind.Utc).AddTicks(8230));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: "3",
                column: "CreatedOn",
                value: new DateTime(2026, 11, 18, 12, 1, 58, 989, DateTimeKind.Utc).AddTicks(8230));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: "1",
                column: "CreatedOn",
                value: new DateTime(2026, 11, 18, 11, 57, 34, 399, DateTimeKind.Utc).AddTicks(5200));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: "2",
                column: "CreatedOn",
                value: new DateTime(2026, 11, 18, 11, 57, 34, 399, DateTimeKind.Utc).AddTicks(5480));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: "3",
                column: "CreatedOn",
                value: new DateTime(2026, 11, 18, 11, 57, 34, 399, DateTimeKind.Utc).AddTicks(5480));
        }
    }
}
