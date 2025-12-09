using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsistanNobetYonetimi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "acildurumlar",
                columns: table => new
                {
                    AcilDurumID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GondermeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BolumAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acildurumlar", x => x.AcilDurumID);
                });

            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "bolumler",
                columns: table => new
                {
                    BolumID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Isim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tanim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kapasite = table.Column<int>(type: "int", nullable: false),
                    HastaSayisi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bolumler", x => x.BolumID);
                });

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

            migrationBuilder.CreateTable(
                name: "asistanlar",
                columns: table => new
                {
                    AsistanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsistanIsimSoyisim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AsistanUnvan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BolumID = table.Column<int>(type: "int", nullable: false),
                    TakvimID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asistanlar", x => x.AsistanID);
                    table.ForeignKey(
                        name: "FK_asistanlar_bolumler_BolumID",
                        column: x => x.BolumID,
                        principalTable: "bolumler",
                        principalColumn: "BolumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_asistanlar_takvim_TakvimID",
                        column: x => x.TakvimID,
                        principalTable: "takvim",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ogretimuyeleri",
                columns: table => new
                {
                    OgretimUyesiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Unvan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsimSoyisim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KullanıcıAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Şifre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BolumID = table.Column<int>(type: "int", nullable: false),
                    TakvimID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ogretimuyeleri", x => x.OgretimUyesiID);
                    table.ForeignKey(
                        name: "FK_ogretimuyeleri_bolumler_BolumID",
                        column: x => x.BolumID,
                        principalTable: "bolumler",
                        principalColumn: "BolumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ogretimuyeleri_takvim_TakvimID",
                        column: x => x.TakvimID,
                        principalTable: "takvim",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "nobetler",
                columns: table => new
                {
                    NobetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsistanID = table.Column<int>(type: "int", nullable: false),
                    BolumID = table.Column<int>(type: "int", nullable: false),
                    NobetTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NobetSaati = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TakvimID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nobetler", x => x.NobetID);
                    table.ForeignKey(
                        name: "FK_nobetler_asistanlar_AsistanID",
                        column: x => x.AsistanID,
                        principalTable: "asistanlar",
                        principalColumn: "AsistanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_nobetler_bolumler_BolumID",
                        column: x => x.BolumID,
                        principalTable: "bolumler",
                        principalColumn: "BolumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_nobetler_takvim_TakvimID",
                        column: x => x.TakvimID,
                        principalTable: "takvim",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "randevular",
                columns: table => new
                {
                    RandevuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsistanID = table.Column<int>(type: "int", nullable: false),
                    OgretimUyesiID = table.Column<int>(type: "int", nullable: false),
                    RandevuTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeSlot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TakvimID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_randevular", x => x.RandevuID);
                    table.ForeignKey(
                        name: "FK_randevular_asistanlar_AsistanID",
                        column: x => x.AsistanID,
                        principalTable: "asistanlar",
                        principalColumn: "AsistanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_randevular_ogretimuyeleri_OgretimUyesiID",
                        column: x => x.OgretimUyesiID,
                        principalTable: "ogretimuyeleri",
                        principalColumn: "OgretimUyesiID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_randevular_takvim_TakvimID",
                        column: x => x.TakvimID,
                        principalTable: "takvim",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_asistanlar_BolumID",
                table: "asistanlar",
                column: "BolumID");

            migrationBuilder.CreateIndex(
                name: "IX_asistanlar_TakvimID",
                table: "asistanlar",
                column: "TakvimID");

            migrationBuilder.CreateIndex(
                name: "IX_nobetler_AsistanID",
                table: "nobetler",
                column: "AsistanID");

            migrationBuilder.CreateIndex(
                name: "IX_nobetler_BolumID",
                table: "nobetler",
                column: "BolumID");

            migrationBuilder.CreateIndex(
                name: "IX_nobetler_TakvimID",
                table: "nobetler",
                column: "TakvimID");

            migrationBuilder.CreateIndex(
                name: "IX_ogretimuyeleri_BolumID",
                table: "ogretimuyeleri",
                column: "BolumID");

            migrationBuilder.CreateIndex(
                name: "IX_ogretimuyeleri_TakvimID",
                table: "ogretimuyeleri",
                column: "TakvimID");

            migrationBuilder.CreateIndex(
                name: "IX_randevular_AsistanID",
                table: "randevular",
                column: "AsistanID");

            migrationBuilder.CreateIndex(
                name: "IX_randevular_OgretimUyesiID",
                table: "randevular",
                column: "OgretimUyesiID");

            migrationBuilder.CreateIndex(
                name: "IX_randevular_TakvimID",
                table: "randevular",
                column: "TakvimID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "acildurumlar");

            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "nobetler");

            migrationBuilder.DropTable(
                name: "randevular");

            migrationBuilder.DropTable(
                name: "asistanlar");

            migrationBuilder.DropTable(
                name: "ogretimuyeleri");

            migrationBuilder.DropTable(
                name: "bolumler");

            migrationBuilder.DropTable(
                name: "takvim");
        }
    }
}
