﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NadinsoftTask.Migrations
{
    /// <inheritdoc />
    public partial class AddProductDatePropertie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ProductDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductDate",
                table: "Products");
        }
    }
}
