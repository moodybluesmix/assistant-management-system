using System;
using System.ComponentModel.DataAnnotations;

namespace AsistanNobetYonetimi.Models
{
    public class Nobet
    {
        [Key] // Primary Key olarak tanımlıyoruz.
        public int NobetID { get; set; } // Nöbet kimlik numarası.
        public int AsistanID { get; set; } // Nöbetteki asistanın kimlik numarası.
        public Asistan? asistan { get; set; } // Foreign Key bağlantısı.
        public int BolumID { get; set; } // Nöbetin tutulduğu bölümün kimlik numarası.
        public Bolum? bolum { get; set; } // Foreign Key bağlantısı.
        public DateTime NobetTarihi { get; set; } // Nöbetin yapıldığı tarih.
        public string NobetSaati { get; set; } // Nöbetin süresi(Saat,Dakika).
    }
}

