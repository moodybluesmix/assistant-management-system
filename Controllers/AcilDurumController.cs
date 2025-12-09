using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsistanNobetYonetimi.Contexts;
using AsistanNobetYonetimi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AsistanNobetYonetimi.Controllers
{
    public class AcilDurumController : Controller
    {
        private readonly NobetDbContext _context;

        public AcilDurumController(NobetDbContext context)
        {
            _context = context;
        }

        public IActionResult Yeni()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Yeni(AcilDurum acilDurum)
        {
            if (ModelState.IsValid)
            {
                _context.acildurumlar.Add(acilDurum);
                await _context.SaveChangesAsync();

                // Tüm ekibe mail gönderme işlemi buraya eklenebilir
                return RedirectToAction("Index");
            }
            return View(acilDurum);
        }
    }

}

