using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistanNobetYonetimi.Models
{
    public class Musaitlik
    {
        [Key]
        public int MusaitlikID { get; set; } // Müsaitlik benzersiz kimlik numarası.

        [Required]
        public int OgretimUyesiID { get; set; } // Müsaitlik oluşturan öğretim üyesinin kimlik numarası.

        [ForeignKey("OgretimUyesiID")]
        public OgretimUyesi OgretimUyesi { get; set; } // Öğretim üyesi ile ilişki.

        [Required]
        public DateTime Tarih { get; set; } // Müsait olunan tarih.

        [Required]
        [MaxLength(50)]
        public string TimeSlot { get; set; } = string.Empty; // Saat dilimi (örn: 14:00-14:30).

        public bool IsAvailable { get; set; } = true; // Randevu için uygun olup olmadığını belirtir.

        // Randevu ile ilişkiyi belirtmek için aşağıdaki koleksiyon tanımını ekleyin.
        public ICollection<Randevu> Randevular { get; set; } = new List<Randevu>();
    }
}
