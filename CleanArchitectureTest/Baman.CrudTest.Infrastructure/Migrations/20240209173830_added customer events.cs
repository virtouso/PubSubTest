using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mc2.CrudTest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedcustomerevents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerEvents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerEvents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerEvents_Email",
                table: "CustomerEvents",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerEvents");
        }
    }
}
