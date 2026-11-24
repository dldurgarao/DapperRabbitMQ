using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DapperRabbitMQ.Migrations
{
    /// <inheritdoc />
    public partial class De : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: "1",
                column: "CreatedOn",
                value: new DateTime(2026, 11, 18, 12, 4, 21, 525, DateTimeKind.Utc).AddTicks(440));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: "2",
                column: "CreatedOn",
                value: new DateTime(2026, 11, 18, 12, 4, 21, 525, DateTimeKind.Utc).AddTicks(610));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: "3",
                column: "CreatedOn",
                value: new DateTime(2026, 11, 18, 12, 4, 21, 525, DateTimeKind.Utc).AddTicks(610));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
