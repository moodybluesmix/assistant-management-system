using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistanNobetYonetimi.Models;
using AsistanNobetYonetimi.Contexts;
using Microsoft.AspNetCore.Authorization;
using AsistanNobetYonetimi.ViewModels;

namespace AsistanNobetYonetimi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly NobetDbContext _context;

        public AdminController(NobetDbContext context)
        {
            _context = context;
        }

        // **1. Admin Dashboard**
        public async Task<IActionResult> Dashboard()
        {
            var viewModel = new DashboardViewModel
            {
                Nobetler = await _context.nobetler
                    .Include(n => n.asistan)
                    .Include(n => n.bolum)
                    .ToListAsync(),
                AcilDurumlar = await _context.acildurumlar
                    .OrderByDescending(a => a.GondermeTarihi)
                    .Take(5)
                    .ToListAsync(),
                ToplamAsistan = await _context.asistanlar.CountAsync(),
                ToplamBolum = await _context.bolumler.CountAsync()
            };

            return View(viewModel);
        }

        // **2. Asistan İşlemleri**
        public async Task<IActionResult> Asistanlar()
        {
            var asistanlar = await _context.asistanlar.Include(a => a.bolum).ToListAsync();
            return View(asistanlar);
        }

        public IActionResult YeniAsistan()
        {
            ViewBag.Bolumler = _context.bolumler.ToList();
            return View(new Asistan());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> YeniAsistan(Asistan asistan)
        {
            if (ModelState.IsValid)
            {
                _context.asistanlar.Add(asistan);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Yeni asistan başarıyla eklendi.";
                return RedirectToAction(nameof(Asistanlar));
            }

            ViewBag.Bolumler = _context.bolumler.ToList();
            return View(asistan);
        }

        public async Task<IActionResult> AsistanDuzenle(int id)
        {
            var asistan = await _context.asistanlar.FindAsync(id);
            if (asistan == null) return NotFound();

            ViewBag.Bolumler = _context.bolumler.ToList();
            return View(asistan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsistanDuzenle(Asistan asistan)
        {
            if (ModelState.IsValid)
            {
                _context.asistanlar.Update(asistan);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Asistan bilgileri başarıyla güncellendi.";
                return RedirectToAction(nameof(Asistanlar));
            }

            ViewBag.Bolumler = _context.bolumler.ToList();
            return View(asistan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsistanSil(int id)
        {
            var asistan = await _context.asistanlar.FindAsync(id);
            if (asistan != null)
            {
                _context.asistanlar.Remove(asistan);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Asistan başarıyla silindi.";
            }
            return RedirectToAction(nameof(Asistanlar));
        }

        // **3. Bölüm İşlemleri**
        public async Task<IActionResult> Bolumler()
        {
            var bolumler = await _context.bolumler.ToListAsync();
            return View(bolumler);
        }

        public IActionResult YeniBolum()
        {
            return View(new Bolum());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> YeniBolum(Bolum bolum)
        {
            if (ModelState.IsValid)
            {
                _context.bolumler.Add(bolum);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Yeni bölüm başarıyla eklendi.";
                return RedirectToAction(nameof(Bolumler));
            }
            return View(bolum);
        }

        public async Task<IActionResult> BolumDuzenle(int id)
        {
            var bolum = await _context.bolumler.FindAsync(id);
            if (bolum == null) return NotFound();

            return View(bolum);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BolumDuzenle(Bolum bolum)
        {
            if (ModelState.IsValid)
            {
                _context.bolumler.Update(bolum);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Bölüm bilgileri başarıyla güncellendi.";
                return RedirectToAction(nameof(Bolumler));
            }
            return View(bolum);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BolumSil(int id)
        {
            var bolum = await _context.bolumler.FindAsync(id);
            if (bolum != null)
            {
                _context.bolumler.Remove(bolum);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Bölüm başarıyla silindi.";
            }
            return RedirectToAction(nameof(Bolumler));
        }

        // **4. Öğretim Üyesi İşlemleri**
        public async Task<IActionResult> OgretimUyeleri()
        {
            var ogretimUyeleri = await _context.ogretimuyeleri.Include(o => o.bolum).ToListAsync();
            return View(ogretimUyeleri);
        }

        public IActionResult YeniOgretimUyesi()
        {
            ViewBag.Bolumler = _context.bolumler.ToList();
            return View(new OgretimUyesi());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> YeniOgretimUyesi(OgretimUyesi ogretimUyesi)
        {
            if (ModelState.IsValid)
            {
                _context.ogretimuyeleri.Add(ogretimUyesi);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Öğretim üyesi başarıyla eklendi.";
                return RedirectToAction(nameof(OgretimUyeleri));
            }

            ViewBag.Bolumler = _context.bolumler.ToList();
            return View(ogretimUyesi);
        }

        public async Task<IActionResult> OgretimUyesiDuzenle(int id)
        {
            var ogretimUyesi = await _context.ogretimuyeleri.FindAsync(id);
            if (ogretimUyesi == null) return NotFound();

            ViewBag.Bolumler = _context.bolumler.ToList();
            return View(ogretimUyesi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OgretimUyesiDuzenle(OgretimUyesi ogretimUyesi)
        {
            if (ModelState.IsValid)
            {
                _context.ogretimuyeleri.Update(ogretimUyesi);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Öğretim üyesi başarıyla güncellendi.";
                return RedirectToAction(nameof(OgretimUyeleri));
            }

            ViewBag.Bolumler = _context.bolumler.ToList();
            return View(ogretimUyesi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OgretimUyesiSil(int id)
        {
            var ogretimUyesi = await _context.ogretimuyeleri.FindAsync(id);
            if (ogretimUyesi != null)
            {
                _context.ogretimuyeleri.Remove(ogretimUyesi);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Öğretim üyesi başarıyla silindi.";
            }
            return RedirectToAction(nameof(OgretimUyeleri));
        }

        // **Yeni Randevu Ekleme (GET)**
        public IActionResult YeniRandevu()
        {
            // Asistan ve Öğretim Üyesi listelerini doldur
            ViewBag.Asistanlar = _context.asistanlar.ToList();
            ViewBag.OgretimUyeleri = _context.ogretimuyeleri.ToList();
            return View(); // Views/Admin/YeniRandevu.cshtml
        }

        // **Yeni Randevu Ekleme (POST)**
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult YeniRandevu(Randevu randevu)
        {
            // Model doğrulama kontrolü
            if (ModelState.IsValid)
            {
                // Yeni randevuyu ekle ve kaydet
                _context.randevular.Add(randevu);
                _context.SaveChanges();
                return RedirectToAction("Randevular"); // Başarılıysa listeye yönlendir
            }

            // Eğer hata varsa ViewBag'leri yeniden doldur
            ViewBag.Asistanlar = _context.asistanlar.ToList();
            ViewBag.OgretimUyeleri = _context.ogretimuyeleri.ToList();
            return View(randevu); // Hatalı formu yeniden yükle
        }

        // **Randevular Listesi**
        public IActionResult Randevular()
        {
            // Tüm randevuları yükle ve ilişkili tabloları dahil et
            var randevular = _context.randevular
                .Include(r => r.asistan)
                .Include(r => r.musaitlik)
                .ToList();

            return View(randevular); // Views/Admin/Randevular.cshtml
        }

        // **Randevu Düzenleme (GET)**
        public IActionResult RandevuDuzenle(int id)
        {
            // Düzenlenecek randevuyu bul
            var randevu = _context.randevular.Find(id);
            if (randevu == null)
            {
                return NotFound();
            }

            // Asistan ve Öğretim Üyesi listelerini doldur
            ViewBag.Asistanlar = _context.asistanlar.ToList();
            ViewBag.OgretimUyeleri = _context.ogretimuyeleri.ToList();
            return View(randevu); // Views/Admin/RandevuDuzenle.cshtml
        }

        // **Randevu Düzenleme (POST)**
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RandevuDuzenle(Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                // Randevuyu güncelle ve kaydet
                _context.randevular.Update(randevu);
                _context.SaveChanges();
                return RedirectToAction("Randevular"); // Başarılıysa listeye yönlendir
            }

            // Hatalı formu yeniden yükle
            ViewBag.Asistanlar = _context.asistanlar.ToList();
            ViewBag.OgretimUyeleri = _context.ogretimuyeleri.ToList();
            return View(randevu);
        }

        // **Randevu Silme**
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RandevuSil(int id)
        {
            // Randevuyu bul ve sil
            var randevu = _context.randevular.Find(id);
            if (randevu != null)
            {
                _context.randevular.Remove(randevu);
                _context.SaveChanges();
            }

            return RedirectToAction("Randevular"); // Listeye yönlendir
        }












        // **1. Nöbet Yönetimi Ana Sayfası**
        public IActionResult NobetYonetimi()
        {
            // Tüm nöbetleri bölümler ve asistanlarla birlikte getir
            var nobetler = _context.nobetler
                .Include(n => n.bolum)
                .Include(n => n.asistan)
                .ToList();

            return View(nobetler); // Views/Admin/NobetYonetimi.cshtml
        }

        // **2. Yeni Nöbet Ekleme (GET)**
        public IActionResult YeniNobet()
        {
            // Bölüm ve asistan listelerini ViewBag ile gönder
            ViewBag.Asistanlar = _context.asistanlar.ToList();
            ViewBag.Bolumler = _context.bolumler.ToList();

            return View(); // Views/Admin/YeniNobet.cshtml
        }

        // **3. Yeni Nöbet Ekleme (POST)**
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult YeniNobet(Nobet nobet)
        {
            if (ModelState.IsValid)
            {
                _context.nobetler.Add(nobet);
                _context.SaveChanges();
                return RedirectToAction(nameof(NobetYonetimi));
            }

            // Form doğrulama başarısızsa tekrar listeleri gönder
            ViewBag.Asistanlar = _context.asistanlar.ToList();
            ViewBag.Bolumler = _context.bolumler.ToList();

            return View(nobet);
        }

        // **4. Nöbet Silme**
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NobetSil(int id)
        {
            var nobet = _context.nobetler.Find(id);
            if (nobet != null)
            {
                _context.nobetler.Remove(nobet);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(NobetYonetimi));
        }






        // **Acil Durumlar Listesi**
        public IActionResult AcilDurumlar()
        {
            var acilDurumlar = _context.acildurumlar.OrderByDescending(a => a.GondermeTarihi).ToList();
            return View(acilDurumlar);
        }

        // **Yeni Acil Durum (GET)**
        public IActionResult YeniAcilDurum()
        {
            return View();
        }

        // **Yeni Acil Durum (POST)**
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult YeniAcilDurum(AcilDurum acilDurum)
        {
            if (ModelState.IsValid)
            {
                acilDurum.GondermeTarihi = DateTime.Now;
                _context.acildurumlar.Add(acilDurum);
                _context.SaveChanges();
                return RedirectToAction("AcilDurumlar");
            }
            return View(acilDurum);
        }

        // **Acil Durum Düzenle (GET)**
        public IActionResult AcilDurumDuzenle(int id)
        {
            var acilDurum = _context.acildurumlar.Find(id);
            if (acilDurum == null)
            {
                return NotFound();
            }
            return View(acilDurum);
        }

        // **Acil Durum Düzenle (POST)**
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AcilDurumDuzenle(AcilDurum acilDurum)
        {
            if (ModelState.IsValid)
            {
                _context.acildurumlar.Update(acilDurum);
                _context.SaveChanges();
                return RedirectToAction("AcilDurumlar");
            }
            return View(acilDurum);
        }

        // **Acil Durum Sil**
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AcilDurumSil(int id)
        {
            var acilDurum = _context.acildurumlar.Find(id);
            if (acilDurum != null)
            {
                _context.acildurumlar.Remove(acilDurum);
                _context.SaveChanges();
            }
            return RedirectToAction("AcilDurumlar");
        }
    }



}
