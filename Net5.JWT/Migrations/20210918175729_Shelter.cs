using Microsoft.EntityFrameworkCore.Migrations;

namespace Net5.JWT.Migrations
{
    public partial class Shelter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShelterRefId",
                table: "Found_Animals",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Comments",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Found_Animals_ShelterRefId",
                table: "Found_Animals",
                column: "ShelterRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Found_Animals_Shelters_ShelterRefId",
                table: "Found_Animals",
                column: "ShelterRefId",
                principalTable: "Shelters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Found_Animals_Shelters_ShelterRefId",
                table: "Found_Animals");

            migrationBuilder.DropIndex(
                name: "IX_Found_Animals_ShelterRefId",
                table: "Found_Animals");

            migrationBuilder.DropColumn(
                name: "ShelterRefId",
                table: "Found_Animals");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Comments",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500);
        }
    }
}
