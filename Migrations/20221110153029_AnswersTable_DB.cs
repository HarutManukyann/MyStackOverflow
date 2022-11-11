using Microsoft.EntityFrameworkCore.Migrations;

namespace SteckOverflow.Migrations
{
    public partial class AnswersTable_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Posts_PostId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Users_AnswerUserId",
                table: "Answers");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Posts_PostId",
                table: "Answers",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Users_AnswerUserId",
                table: "Answers",
                column: "AnswerUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Posts_PostId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Users_AnswerUserId",
                table: "Answers");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Posts_PostId",
                table: "Answers",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Users_AnswerUserId",
                table: "Answers",
                column: "AnswerUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
