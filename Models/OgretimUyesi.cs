using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistanNobetYonetimi.Models
{
    public class OgretimUyesi
    {
        [Key] // Primary Key tanımlıyoruz.
        public int OgretimUyesiID { get; set; } // Öğretim üyesinin kimlik numarası.
        public string Unvan { get; set; }
        public string IsimSoyisim { get; set; } 
        public string? Resim { get; set; } // Resim.
        public string? Telefon { get; set; } // Telefon No.
        public string? Email { get; set; } // Mail Adresi.
        public string? KullaniciAdi { get; set; } // Kullanıcı adını admin panelinde görüntüleyebiliriz.
        public string? Sifre { get; set; } // Şifreyi admin panelinde görüntüleyebiliriz.
        public int BolumID { get; set; } // Öğretim üyesinin bağlı olduğu bölümün numarası. Foreign Key.
        [ForeignKey("BolumID")]
        public Bolum? bolum { get; set; }
        public ICollection<Randevu> randevu { get; set; } = new List<Randevu>();// Öğretim Üyesinin gireceği randevunun numarası. Foreign Key.
        // Musaitlik ilişkisi
        public ICollection<Musaitlik> musaitlikler { get; set; } = new List<Musaitlik>();
    } 
}

