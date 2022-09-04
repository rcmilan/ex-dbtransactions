using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ex_dbtransactions.Migrations
{
    public partial class Addresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_District",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Address_Number",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "People");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Street = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    District = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_Addresses_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.AddColumn<string>(
                name: "Address_District",
                table: "People",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Address_Number",
                table: "People",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "People",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
