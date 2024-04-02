using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMovieCommentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MovieComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MovieComments_UserId",
                table: "MovieComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieComments_AspNetUsers_UserId",
                table: "MovieComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieComments_AspNetUsers_UserId",
                table: "MovieComments");

            migrationBuilder.DropIndex(
                name: "IX_MovieComments_UserId",
                table: "MovieComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MovieComments");
        }
    }
}
