using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6dbe5ecc-f395-416a-a5fc-67aae7e82424");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b51ff84d-3115-4c98-92c8-e3b452060188");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37b9914e-72ed-4606-ac72-3b48094046a3", null, "User", "USER" },
                    { "dfa405c2-a01d-4b2a-b1c5-f8354d31c0d2", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37b9914e-72ed-4606-ac72-3b48094046a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfa405c2-a01d-4b2a-b1c5-f8354d31c0d2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6dbe5ecc-f395-416a-a5fc-67aae7e82424", null, "User", "USER" },
                    { "b51ff84d-3115-4c98-92c8-e3b452060188", null, "Admin", "ADMIN" }
                });
        }
    }
}
