using System;
using System.ComponentModel.DataAnnotations;
using AsistanNobetYonetimi.Models;

namespace AsistanNobetYonetimi.ViewModels
{
	public class RandevuYonetimViewModel
	{
        
        
            public IEnumerable<Musaitlik> Musaitlikler { get; set; }
            public IEnumerable<Randevu> Randevular { get; set; }
        [Required]
        public int MusaitlikID { get; set; }

        [Required]
        public string RandevuTarihi { get; set; }

        [Required]
        public string TimeSlot { get; set; }

        [Required]
        public string OgretimUyesi { get; set; }
    }
}

    


