using System;
using System.ComponentModel.DataAnnotations;

namespace AsistanNobetYonetimi.Models
{
    public class Bolum
    {
        [Key] // Primary Key olarak BolumId'yi atıyoruz.
        public int BolumID { get; set; } // Bölüm kimlik numarası (örn. Çocuk Acil, Çocuk Yoğun Bakımı).
        public string Isim { get; set; } // Bölümün adı.
        public string Tanim { get; set; } // Bölümün açıklaması.
        public int Kapasite { get; set; } // Yatak kapasitesi veya boş yatak sayısı gibi bilgiler.
        public int HastaSayisi { get; set; } // Mevcut hasta sayısı.
        public ICollection<Asistan> asistan { get; set; } // Asistanlara Foreign Key bağlantısını sağlıyoruz.
        public ICollection<OgretimUyesi> ogretimuyesi { get; set; } // Öğretim üyelerine Foreign Key bağlantısını sağlıyoruz.
        public ICollection<Nobet> nobet { get; set; } // Nöbetlere Foreign Key bağlantısını sağlıyoruz.
    }
}

