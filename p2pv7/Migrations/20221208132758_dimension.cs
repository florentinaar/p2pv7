using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace p2pv7.Migrations
{
    /// <inheritdoc />
    public partial class dimension : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dimensions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    width = table.Column<double>(type: "float", nullable: false),
                    height = table.Column<double>(type: "float", nullable: false),
                    length = table.Column<double>(type: "float", nullable: false),
                    inUse = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimensions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dimensions");
        }
    }
}
