﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace p2pv7.Migrations
{
    /// <inheritdoc />
    public partial class tokenverifyinuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VerifiedAt",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VerifiedAt",
                table: "Users");
        }
    }
}
