using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsistanNobetYonetimi.Migrations
{
    public partial class AddIsAvailableToRandevu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TimeSlot",
                table: "randevular",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "randevular",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "randevular");

            migrationBuilder.AlterColumn<string>(
                name: "TimeSlot",
                table: "randevular",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
