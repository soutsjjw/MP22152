using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleCodeFirst.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "UserName" },
                values: new object[] { new Guid("06158c72-3f2e-4d6f-8af6-96d1354a77ce"), "ATai" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Content", "PostTime", "Read", "Title", "UserId" },
                values: new object[] { 1, "第一次學ASP.NET Core就上手", new DateTime(2021, 11, 24, 14, 30, 0, 0, DateTimeKind.Unspecified), 0, "ASP.NET Core", new Guid("06158c72-3f2e-4d6f-8af6-96d1354a77ce") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("06158c72-3f2e-4d6f-8af6-96d1354a77ce"));
        }
    }
}
