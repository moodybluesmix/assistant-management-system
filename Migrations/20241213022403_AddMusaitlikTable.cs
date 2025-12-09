using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsistanNobetYonetimi.Migrations
{
    public partial class AddMusaitlikTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asistanlar_takvim_TakvimID",
                table: "asistanlar");

            migrationBuilder.DropForeignKey(
                name: "FK_nobetler_takvim_TakvimID",
                table: "nobetler");

            migrationBuilder.DropForeignKey(
                name: "FK_ogretimuyeleri_takvim_TakvimID",
                table: "ogretimuyeleri");

            migrationBuilder.DropForeignKey(
                name: "FK_randevular_ogretimuyeleri_OgretimUyesiID",
                table: "randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_randevular_takvim_TakvimID",
                table: "randevular");

            migrationBuilder.DropTable(
                name: "takvim");

            migrationBuilder.DropIndex(
                name: "IX_randevular_TakvimID",
                table: "randevular");

            migrationBuilder.DropIndex(
                name: "IX_ogretimuyeleri_TakvimID",
                table: "ogretimuyeleri");

            migrationBuilder.DropIndex(
                name: "IX_nobetler_TakvimID",
                table: "nobetler");

            migrationBuilder.DropIndex(
                name: "IX_asistanlar_TakvimID",
                table: "asistanlar");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "randevular");

            migrationBuilder.DropColumn(
                name: "TakvimID",
                table: "randevular");

            migrationBuilder.DropColumn(
                name: "TimeSlot",
                table: "randevular");

            migrationBuilder.DropColumn(
                name: "TakvimID",
                table: "ogretimuyeleri");

            migrationBuilder.DropColumn(
                name: "TakvimID",
                table: "nobetler");

            migrationBuilder.DropColumn(
                name: "TakvimID",
                table: "asistanlar");

            migrationBuilder.AlterColumn<int>(
                name: "OgretimUyesiID",
                table: "randevular",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MusaitlikID",
                table: "randevular",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "musaitlikler",
                columns: table => new
                {
                    MusaitlikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgretimUyesiID = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeSlot = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_musaitlikler", x => x.MusaitlikID);
                    table.ForeignKey(
                        name: "FK_musaitlikler_ogretimuyeleri_OgretimUyesiID",
                        column: x => x.OgretimUyesiID,
                        principalTable: "ogretimuyeleri",
                        principalColumn: "OgretimUyesiID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_randevular_MusaitlikID",
                table: "randevular",
                column: "MusaitlikID");

            migrationBuilder.CreateIndex(
                name: "IX_musaitlikler_OgretimUyesiID",
                table: "musaitlikler",
                column: "OgretimUyesiID");

            migrationBuilder.AddForeignKey(
                name: "FK_randevular_musaitlikler_MusaitlikID",
                table: "randevular",
                column: "MusaitlikID",
                principalTable: "musaitlikler",
                principalColumn: "MusaitlikID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_randevular_ogretimuyeleri_OgretimUyesiID",
                table: "randevular",
                column: "OgretimUyesiID",
                principalTable: "ogretimuyeleri",
                principalColumn: "OgretimUyesiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_randevular_musaitlikler_MusaitlikID",
                table: "randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_randevular_ogretimuyeleri_OgretimUyesiID",
                table: "randevular");

            migrationBuilder.DropTable(
                name: "musaitlikler");

            migrationBuilder.DropIndex(
                name: "IX_randevular_MusaitlikID",
                table: "randevular");

            migrationBuilder.DropColumn(
                name: "MusaitlikID",
                table: "randevular");

            migrationBuilder.AlterColumn<int>(
                name: "OgretimUyesiID",
                table: "randevular",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "randevular",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TakvimID",
                table: "randevular",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeSlot",
                table: "randevular",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TakvimID",
                table: "ogretimuyeleri",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TakvimID",
                table: "nobetler",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TakvimID",
                table: "asistanlar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "takvim",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_takvim", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_randevular_TakvimID",
                table: "randevular",
                column: "TakvimID");

            migrationBuilder.CreateIndex(
                name: "IX_ogretimuyeleri_TakvimID",
                table: "ogretimuyeleri",
                column: "TakvimID");

            migrationBuilder.CreateIndex(
                name: "IX_nobetler_TakvimID",
                table: "nobetler",
                column: "TakvimID");

            migrationBuilder.CreateIndex(
                name: "IX_asistanlar_TakvimID",
                table: "asistanlar",
                column: "TakvimID");

            migrationBuilder.AddForeignKey(
                name: "FK_asistanlar_takvim_TakvimID",
                table: "asistanlar",
                column: "TakvimID",
                principalTable: "takvim",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_nobetler_takvim_TakvimID",
                table: "nobetler",
                column: "TakvimID",
                principalTable: "takvim",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ogretimuyeleri_takvim_TakvimID",
                table: "ogretimuyeleri",
                column: "TakvimID",
                principalTable: "takvim",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_randevular_ogretimuyeleri_OgretimUyesiID",
                table: "randevular",
                column: "OgretimUyesiID",
                principalTable: "ogretimuyeleri",
                principalColumn: "OgretimUyesiID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_randevular_takvim_TakvimID",
                table: "randevular",
                column: "TakvimID",
                principalTable: "takvim",
                principalColumn: "ID");
        }
    }
}
