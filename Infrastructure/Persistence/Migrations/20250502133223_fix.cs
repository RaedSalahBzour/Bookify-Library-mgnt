using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryBooks",
                table: "CategoryBooks");

            migrationBuilder.DropIndex(
                name: "IX_CategoryBooks_BookId",
                table: "CategoryBooks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryBooks",
                table: "CategoryBooks",
                columns: new[] { "BookId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryBooks_CategoryId",
                table: "CategoryBooks",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryBooks",
                table: "CategoryBooks");

            migrationBuilder.DropIndex(
                name: "IX_CategoryBooks_CategoryId",
                table: "CategoryBooks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryBooks",
                table: "CategoryBooks",
                columns: new[] { "CategoryId", "BookId" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryBooks_BookId",
                table: "CategoryBooks",
                column: "BookId");
        }
    }
}
