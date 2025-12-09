using System;
using System.ComponentModel.DataAnnotations;

namespace AsistanNobetYonetimi.Models
{
	public class AcilDurum
	{
        [Key]
        public int AcilDurumID { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public DateTime GondermeTarihi { get; set; }
        public string BolumAdi { get; set; }
    }
}

