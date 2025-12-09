using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AsistanNobetYonetimi.Contexts;
using AsistanNobetYonetimi.Models;
using AsistanNobetYonetimi.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsistanNobetYonetimi.Controllers
{
    public class AsistanController : Controller
    {
        private readonly NobetDbContext _context;

        public AsistanController(NobetDbContext context)
        {
            _context = context;
        }

        // Login Sayfası (GET)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Login İşlemi (POST)
        [HttpPost]
        public async Task<IActionResult> Login(string kullaniciAdi, string sifre)
        {
            var asistan = await _context.asistanlar
                .FirstOrDefaultAsync(a => a.KullaniciAdi == kullaniciAdi && a.Sifre == sifre);

            if (asistan != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, asistan.KullaniciAdi),
                    new Claim("Role", "Asistan")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Dashboard", "Asistan");
            }

            TempData["Error"] = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }

        // Logout İşlemi
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // Dashboard Sayfası
        public async Task<IActionResult> Dashboard()
        {
            var kullaniciAdi = User.Identity.Name;

            var asistan = await _context.asistanlar
                .Include(a => a.bolum)
                .Include(a => a.nobet)
                .FirstOrDefaultAsync(a => a.KullaniciAdi == kullaniciAdi);

            if (asistan == null)
            {
                TempData["Error"] = "Asistan bilgisi bulunamadı.";
                return RedirectToAction("Login");
            }

            return View(asistan);
        }

        // Profil Görüntüleme
        public async Task<IActionResult> Profil()
        {
            var kullaniciAdi = User.Identity.Name;
            var asistan = await _context.asistanlar
                .Include(a => a.bolum)
                .FirstOrDefaultAsync(a => a.KullaniciAdi == kullaniciAdi);

            if (asistan == null)
                return RedirectToAction("Login");

            return View(asistan);
        }

        // Randevu Yönetimi Sayfası
        [HttpGet]
        public async Task<IActionResult> RandevuYonetimi()
        {
            var kullaniciAdi = User.Identity.Name;
            var asistan = await _context.asistanlar
                .Include(a => a.randevu)
                .ThenInclude(r => r.musaitlik)
                .ThenInclude(m => m.OgretimUyesi)
                .FirstOrDefaultAsync(a => a.KullaniciAdi == kullaniciAdi);

            if (asistan == null)
            {
                TempData["Error"] = "Asistan bilgisi bulunamadı.";
                return RedirectToAction("Login");
            }

            var viewModel = new RandevuYonetimViewModel
            {
                Musaitlikler = await _context.musaitlikler
                    .Include(m => m.OgretimUyesi)
                    .Where(m => m.IsAvailable)
                    .ToListAsync(),
                Randevular = asistan.randevu
            };

            return View(viewModel);
        }

        // Randevu Talep Etme (POST)
        [HttpPost]
        public IActionResult RandevuTalep(RandevuYonetimViewModel model)
        {
            if (ModelState.IsValid)
            {
                DateTime randevuTarihi;
                if (!DateTime.TryParse(model.RandevuTarihi, out randevuTarihi))
                {
                    ModelState.AddModelError("", "Randevu tarihi geçerli bir formatta değil.");
                    return View("RandevuYonetimi", model);
                }

                var yeniRandevu = new Randevu
                {
                    MusaitlikID = model.MusaitlikID,
                    RandevuTarihi = randevuTarihi
                };

                try
                {
                    _context.randevular.Add(yeniRandevu);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Randevu başarıyla talep edildi.";
                    return RedirectToAction("RandevuYonetimi");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Veritabanına kaydedilirken bir hata oluştu: " + ex.Message);
                    return View("RandevuYonetimi", model);
                }
            }

            return View("RandevuYonetimi", model);
        }

        // Müsaitlik Listesi
        [HttpGet]
        public async Task<IActionResult> Musaitlikler()
        {
            var musaitlikler = await _context.musaitlikler
                .Include(m => m.OgretimUyesi)
                .Where(m => m.IsAvailable)
                .ToListAsync();

            return View(musaitlikler);
        }

        // Randevular Sayfası
        [HttpGet]
        public async Task<IActionResult> Randevular()
        {
            var kullaniciAdi = User.Identity.Name;
            var asistan = await _context.asistanlar
                .Include(a => a.randevu)
                .ThenInclude(r => r.musaitlik)
                .ThenInclude(m => m.OgretimUyesi)
                .FirstOrDefaultAsync(a => a.KullaniciAdi == kullaniciAdi);

            if (asistan == null)
            {
                TempData["Error"] = "Asistan bilgisi bulunamadı.";
                return RedirectToAction("Login");
            }

            return View(asistan.randevu);
        }

        // Acil durum duyurularını görüntüleme
        public IActionResult AcilDurumlar()
        {
            var acilDurumlar = _context.acildurumlar.OrderByDescending(a => a.GondermeTarihi).ToList();
            return View(acilDurumlar);
        }
    }
}
