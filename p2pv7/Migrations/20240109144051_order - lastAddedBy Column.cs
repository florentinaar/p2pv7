using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace p2pv7.Migrations
{
    /// <inheritdoc />
    public partial class orderlastAddedByColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "width",
                table: "Dimensions",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Dimensions",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "length",
                table: "Dimensions",
                newName: "Length");

            migrationBuilder.RenameColumn(
                name: "inUse",
                table: "Dimensions",
                newName: "InUse");

            migrationBuilder.RenameColumn(
                name: "height",
                table: "Dimensions",
                newName: "Height");

            migrationBuilder.AddColumn<Guid>(
                name: "LastEditedBy",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LastStatusSetBy",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastEditedBy",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LastStatusSetBy",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Dimensions",
                newName: "width");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Dimensions",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Length",
                table: "Dimensions",
                newName: "length");

            migrationBuilder.RenameColumn(
                name: "InUse",
                table: "Dimensions",
                newName: "inUse");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Dimensions",
                newName: "height");
        }
    }
}
