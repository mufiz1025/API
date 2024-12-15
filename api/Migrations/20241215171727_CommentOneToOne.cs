using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class CommentOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37b9914e-72ed-4606-ac72-3b48094046a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfa405c2-a01d-4b2a-b1c5-f8354d31c0d2");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "29f1d2a7-f9ed-435e-9abb-16a97ee3338a", null, "Admin", "ADMIN" },
                    { "566e4037-b788-43ad-85e3-bade6f2a37dc", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AppUserId",
                table: "Comment",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_AppUserId",
                table: "Comment",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_AppUserId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_AppUserId",
                table: "Comment");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29f1d2a7-f9ed-435e-9abb-16a97ee3338a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "566e4037-b788-43ad-85e3-bade6f2a37dc");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Comment");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37b9914e-72ed-4606-ac72-3b48094046a3", null, "User", "USER" },
                    { "dfa405c2-a01d-4b2a-b1c5-f8354d31c0d2", null, "Admin", "ADMIN" }
                });
        }
    }
}
