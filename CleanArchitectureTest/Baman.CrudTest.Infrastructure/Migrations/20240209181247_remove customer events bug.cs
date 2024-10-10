using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mc2.CrudTest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removecustomereventsbug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerEvents_Email",
                table: "CustomerEvents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CustomerEvents_Email",
                table: "CustomerEvents",
                column: "Email",
                unique: true);
        }
    }
}
