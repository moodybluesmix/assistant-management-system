using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using AsistanNobetYonetimi.Contexts;

public class OgretimUyesiController : Controller
{
    private readonly NobetDbContext _context;

    public OgretimUyesiController(NobetDbContext context)
    {
        _context = context;
    }

    // Login Sayfası (GET)
    [HttpGet]
    public IActionResult Login()
    {
        return View(); // Views/OgretimUyesi/Login.cshtml
    }

    // Login İşlemi (POST)
    [HttpPost]
    public async Task<IActionResult> Login(string kullaniciAdi, string sifre)
    {
        var ogretimUyesi = await _context.ogretimuyeleri
            .FirstOrDefaultAsync(o => o.KullaniciAdi == kullaniciAdi && o.Sifre == sifre);

        if (ogretimUyesi != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, ogretimUyesi.KullaniciAdi),
                new Claim("Role", "OgretimUyesi")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Dashboard", "OgretimUyesi");
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

    // Dashboard Sayfası (GET)
    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        var kullaniciAdi = User.Identity.Name; // Giriş yapan öğretim üyesinin kullanıcı adı

        var ogretimUyesi = await _context.ogretimuyeleri
            .Include(o => o.bolum) // Sorumlu olduğu bölümleri yüklüyoruz
                .ThenInclude(b => b.asistan) // Bölümdeki asistanları yüklüyoruz
            .Include(o => o.bolum)
                .ThenInclude(b => b.nobet) // Bölümdeki nöbet bilgilerini yüklüyoruz
            .FirstOrDefaultAsync(o => o.KullaniciAdi == kullaniciAdi);

        if (ogretimUyesi == null)
        {
            return RedirectToAction("Login");
        }

        return View("Dashboard", ogretimUyesi); // Views/OgretimUyesi/Dashboard.cshtml
    }
}
