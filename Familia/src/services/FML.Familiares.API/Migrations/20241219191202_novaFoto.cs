using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FML.Familiares.API.Migrations
{
    /// <inheritdoc />
    public partial class novaFoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FotoUrl",
                table: "Relatives",
                newName: "FotoStream");

            migrationBuilder.AddColumn<Guid>(
                name: "FotoId",
                table: "Relatives",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoId",
                table: "Relatives");

            migrationBuilder.RenameColumn(
                name: "FotoStream",
                table: "Relatives",
                newName: "FotoUrl");
        }
    }
}
