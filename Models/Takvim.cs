using System;
using System.ComponentModel.DataAnnotations;

namespace AsistanNobetYonetimi.Models
{
	public class Takvim
	{
        [Key]
        public int ID { get; set; }
        public List<Nobet> nobet { get; set; }
        public List<Asistan> asistan { get; set; }
        public List<OgretimUyesi> ogretimuyesi { get; set; }
        public List<Randevu> randevu { get; set; }
    }
}

