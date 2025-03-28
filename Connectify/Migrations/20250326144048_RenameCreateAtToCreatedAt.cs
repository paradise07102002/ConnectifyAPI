﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectify.Migrations
{
    /// <inheritdoc />
    public partial class RenameCreateAtToCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Comments",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Comments",
                newName: "CreateAt");
        }
    }
}
