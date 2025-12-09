using AsistanNobetYonetimi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistanNobetYonetimi.Models
{
    public class Randevu
    {
        [Key]
        public int RandevuID { get; set; } // Randevunun benzersiz kimlik numarası.

        [Required]
        public int MusaitlikID { get; set; } // İlişkili müsaitlik bilgisi.

        [ForeignKey("MusaitlikID")]
        public Musaitlik musaitlik { get; set; } // Müsaitlik ile ilişki.

        [Required]
        public int AsistanID { get; set; } // Randevuyu talep eden asistanın kimlik numarası.

        [ForeignKey("AsistanID")]
        public Asistan asistan { get; set; } // Asistan ile ilişki.

        [Required]
        public DateTime RandevuTarihi { get; set; } // Randevunun tarihi.

    }
}