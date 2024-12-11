using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace p2pv7.Migrations
{
    /// <inheritdoc />
    public partial class Rolesremoval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28c47b83-1dd0-42ce-8b01-3b702776ea8a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a9f483f-5db4-4b50-94cb-a81e75b156fb");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28c47b83-1dd0-42ce-8b01-3b702776ea8a", null, "Administrator", "ADMINISTRATOR" },
                    { "5a9f483f-5db4-4b50-94cb-a81e75b156fb", null, "User", "USER" }
                });
        }
    }
}
