using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crudStudiesNET.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDateTimeModelTodo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Todos",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Todos");
        }
    }
}
