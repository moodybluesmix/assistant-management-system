using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace AsistanNobetYonetimi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Views/Home/Index.cshtml dosyasını döndürür
        }

        public IActionResult Login()
        {
            return View(); // Views/Home/Login.cshtml dosyasını döndürür
        }

        // Giriş İşlemi (POST)
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Örnek kullanıcı kontrolü
            if (username == "admin" && password == "12345") // Sabit kullanıcı bilgileri
            {
                // Kullanıcı bilgileri ve rolleri
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                // Kullanıcıyı oturum açtır
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Dashboard", "Admin"); // Admin paneline yönlendir
            }

            // Geçersiz giriş için hata mesajı
            ViewBag.Error = "Geçersiz kullanıcı adı veya şifre!";
            return View();
        }

        // Çıkış İşlemi
        public async Task<IActionResult> Logout()
        {
            // Kullanıcının oturumunu kapat
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // Yetkisiz Erişim Sayfası
        public IActionResult AccessDenied()
        {
            return View(); // Views/Home/AccessDenied.cshtml dosyasını döndürür
        }
    }
}
    