using AsistanNobetYonetimi.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BolumController : Controller
{
    private readonly NobetDbContext _context;

    public BolumController(NobetDbContext context)
    {
        _context = context;
    }

    // Bölüm Tanıtımı
    public async Task<IActionResult> BolumTanitimi(int id)
    {
        var bolum = await _context.bolumler
            .Include(b => b.ogretimuyesi) // Bölümün öğretim üyelerini dahil et
            .FirstOrDefaultAsync(b => b.BolumID == id);

        if (bolum == null)
        {
            return NotFound(); // Bölüm bulunamazsa
        }

        return View(bolum); // Views/Bolum/BolumTanitimi.cshtml
    }
}
