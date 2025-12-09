using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsistanNobetYonetimi.Models;
using Microsoft.AspNetCore.Mvc;
using AsistanNobetYonetimi.Contexts;
using Microsoft.EntityFrameworkCore;


namespace AsistanNobetYonetimi.Controllers
{
    public class NobetController : Controller
    {
        private readonly NobetDbContext _context;

        public NobetController(NobetDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Takvim()
        {
            var nobetler = await _context.nobetler.Include(n => n.asistan).Include(n => n.bolum).ToListAsync();
            return View(nobetler);
        }
    }

}

