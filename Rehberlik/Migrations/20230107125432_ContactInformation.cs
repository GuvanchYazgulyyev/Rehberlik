using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rehberlik.Migrations
{
    /// <inheritdoc />
    public partial class ContactInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Face",
                table: "ContactInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ins",
                table: "ContactInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Linkd",
                table: "ContactInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Twit",
                table: "ContactInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Face",
                table: "ContactInformation");

            migrationBuilder.DropColumn(
                name: "Ins",
                table: "ContactInformation");

            migrationBuilder.DropColumn(
                name: "Linkd",
                table: "ContactInformation");

            migrationBuilder.DropColumn(
                name: "Twit",
                table: "ContactInformation");
        }
    }
}
