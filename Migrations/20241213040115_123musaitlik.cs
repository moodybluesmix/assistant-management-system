using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsistanNobetYonetimi.Migrations
{
    public partial class _123musaitlik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_randevular_musaitlikler_MusaitlikID",
                table: "randevular");

            migrationBuilder.AddForeignKey(
                name: "FK_randevular_musaitlikler_MusaitlikID",
                table: "randevular",
                column: "MusaitlikID",
                principalTable: "musaitlikler",
                principalColumn: "MusaitlikID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_randevular_musaitlikler_MusaitlikID",
                table: "randevular");

            migrationBuilder.AddForeignKey(
                name: "FK_randevular_musaitlikler_MusaitlikID",
                table: "randevular",
                column: "MusaitlikID",
                principalTable: "musaitlikler",
                principalColumn: "MusaitlikID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
