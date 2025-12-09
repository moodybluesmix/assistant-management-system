using System;
using System.ComponentModel.DataAnnotations;

namespace AsistanNobetYonetimi.Models
{
	public class Admin
	{
        [Key]
        public int AdminID { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
    }
}

