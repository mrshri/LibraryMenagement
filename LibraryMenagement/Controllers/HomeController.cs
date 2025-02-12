using System.Diagnostics;
using LibraryMenagement.DATA;
using LibraryMenagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryMenagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LibraryContext _context;

        public HomeController(ILogger<HomeController> logger,LibraryContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var books = await _context.Books
                    .Include(b => b.BorrowRecords)
                    .AsNoTracking()
                    .ToListAsync();
                return View(books);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the books.";
                return View("Error");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
