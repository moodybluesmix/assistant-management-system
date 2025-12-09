using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace AsistanNobetYonetimi.Models
{
	public class Asistan
	{
        [Key]
        public int AsistanID { get; set; }
        public string AsistanIsimSoyisim { get; set; }
        public string AsistanUnvan { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }

        public int BolumID { get; set; } // Asistanın bağlı olduğu bölümün numarası. Foreign Key.
        public Bolum? bolum { get; set; }
        public ICollection<Nobet> nobet { get; set; } = new List<Nobet>();// bir asistan birden fazla nobet yaoabilir. 
        public ICollection<Randevu> randevu { get; set; } = new List<Randevu>();// Asistanın gireceği randevunun numarası. Foreign Key.bir asistan birden fazla randevusu olabilir.

    }
}

