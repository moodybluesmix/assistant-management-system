using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsistanNobetYonetimi.Migrations
{
    public partial class migkullanici : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Şifre",
                table: "ogretimuyeleri",
                newName: "Sifre");

            migrationBuilder.RenameColumn(
                name: "KullanıcıAdi",
                table: "ogretimuyeleri",
                newName: "KullaniciAdi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sifre",
                table: "ogretimuyeleri",
                newName: "Şifre");

            migrationBuilder.RenameColumn(
                name: "KullaniciAdi",
                table: "ogretimuyeleri",
                newName: "KullanıcıAdi");
        }
    }
}
