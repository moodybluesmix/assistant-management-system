using System;
using AsistanNobetYonetimi.Models;

namespace AsistanNobetYonetimi.ViewModels
{
	public class DashboardViewModel
	{
        public List<Nobet> Nobetler { get; set; }
        public List<AcilDurum> AcilDurumlar { get; set; }
        public int ToplamAsistan { get; internal set; }
        public int ToplamBolum { get; internal set; }
    }
}

