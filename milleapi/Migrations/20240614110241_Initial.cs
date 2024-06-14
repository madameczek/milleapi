using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace milleapi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedOn", "FirstName", "IsDeleted", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 14, 11, 2, 41, 440, DateTimeKind.Utc).AddTicks(8195), "Jan", false, "Kowalski" },
                    { 2, new DateTime(2024, 6, 14, 11, 2, 41, 440, DateTimeKind.Utc).AddTicks(8196), "Adam", false, "Adamski" },
                    { 3, new DateTime(2024, 6, 14, 11, 2, 41, 440, DateTimeKind.Utc).AddTicks(8197), "Marcin", false, "Nowak" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
